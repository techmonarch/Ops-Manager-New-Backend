using OpsManagerAPI.Application.Features.BillDistributions.Dtos;

namespace OpsManagerAPI.Application.Features.BillDistributions.Queries;
public class BillDistributionQueries : IBillDistributionQueries
{
    #region Constructor
    private readonly IBillDistributionRepository _repository;
    private readonly IDapperRepository _dapperRepository;
    public BillDistributionQueries(IDapperRepository dapperRepository, IBillDistributionRepository repository)
    {
        _dapperRepository = dapperRepository;
        _repository = repository;
    }
    #endregion

    #region Dapper
    public async Task<ApiResponse<BillDistributionDashboardDto>> Dashboard()
    {
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        const string sql = @"
                            DECLARE @PostPaidCustomersCount INT;
                            DECLARE @DistributedBillsCount INT;

                            SELECT @PostPaidCustomersCount = COUNT(DISTINCT c.Id) 
                            FROM [Customers] c
                            WHERE c.AccountType = 2;

                            SELECT @DistributedBillsCount = COUNT(*) 
                            FROM [BillDistributions] bd
                            WHERE MONTH(bd.DistributionDate) = @CurrentMonth 
                              AND YEAR(bd.DistributionDate) = @CurrentYear;

                            SELECT
                                @PostPaidCustomersCount AS PostPaidCustomersCount,
                                @DistributedBillsCount AS DistributedBillsCount,
                                (@PostPaidCustomersCount - @DistributedBillsCount) AS PendingBillsCount,

                            (SELECT COUNT(*) 
                                FROM [BillDistributions] bd
                                WHERE CONVERT(DATE, bd.DistributionDate) = @CurrentDate) AS TodayCount;";

        var parameters = new { CurrentMonth = currentMonth, CurrentYear = currentYear, CurrentDate = DateTime.Now.Date };

        var dashboardData = await _dapperRepository.QueryFirstOrDefaultAsync<BillDistributionDashboardDto>(sql, parameters);

        if (dashboardData == null)
        {
            return new ApiResponse<BillDistributionDashboardDto>(false, "Bill distributions dashboard data not found", default!);
        }

        return new ApiResponse<BillDistributionDashboardDto>(true, "Bill distributions dashboard data retrieved successfully", dashboardData);
    }
    #endregion

    #region EF-Core
    public async Task<ApiResponse<PaginationResponse<BillDistributionDetailDto>>> SearchAsync(BillDistributionFilterRequest request, CancellationToken cancellationToken)
    {
        var billDistributions = await _repository.SearchAsync(request, cancellationToken);

        // Map billDistributions to billDistributionDetailDto
        var billDistributionDataList = billDistributions.ConvertAll(billDistribution =>
        {
            return new BillDistributionDetailDto(
                FirstName: billDistribution?.Customer?.FirstName!,
                LastName: billDistribution?.Customer?.LastName!,
                AccountNumber: billDistribution?.Customer?.AccountNumber!,
                MeterNumber: billDistribution?.Customer?.MeterNumber!,
                Tariff: billDistribution?.Customer?.Tariff?.UniqueId!,
                DSSName: billDistribution.Customer?.DistributionTransformer?.Name!,
                BillAmount: billDistribution.BillAmount,
                Longitude: billDistribution.Longitude,
                Latitude: billDistribution.Latitude,
                DistributionDate: billDistribution.DistributionDate,
                IsDelivered: billDistribution.IsDelivered);
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<BillDistributionDetailDto>(
            billDistributionDataList,
            billDistributionDataList.Count,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<BillDistributionDetailDto>>(true, "Bill distributions retrieved successfully", paginationResponse);
    }

    #endregion
}
