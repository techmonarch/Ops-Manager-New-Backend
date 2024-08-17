using OpsManagerAPI.Application.Features.MeterReadings.Dtos;

namespace OpsManagerAPI.Application.Features.MeterReadings.Queries;
public class MeterReadingQueries : IMeterReadingQueries
{
    #region Constructor
    private readonly IDapperRepository _dapperRepository;
    private readonly IMeterReadingRepository _repository;

    public MeterReadingQueries(IDapperRepository dapperRepository, IMeterReadingRepository repository)
    {
        _dapperRepository = dapperRepository;
        _repository = repository;
    }
    #endregion

    #region Dapper
    public async Task<ApiResponse<MeterReadingDashboardDto>> Dashboard()
    {
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        const string sql = @"
                            DECLARE @PostPaidCustomersCount INT;
                            DECLARE @MeterReadCount INT;

                            SELECT @PostPaidCustomersCount = COUNT(DISTINCT c.Id) 
                            FROM [Customers] c
                            WHERE c.AccountType = 2;

                            SELECT @MeterReadCount = COUNT(*) 
                            FROM [MeterReadings] mr
                            WHERE MONTH(mr.ReadingDate) = @CurrentMonth 
                              AND YEAR(mr.ReadingDate) = @CurrentYear;

                            SELECT
                                @PostPaidCustomersCount AS PostPaidCustomersCount,
                                @MeterReadCount AS MeterReadCount,
                                (@PostPaidCustomersCount - @MeterReadCount) AS PendingReadingCount,

                            (SELECT COUNT(*) 
                                FROM [MeterReadings] mr
                                WHERE CONVERT(DATE, mr.ReadingDate) = @CurrentDate) AS TodayCount;";

        var parameters = new { CurrentMonth = currentMonth, CurrentYear = currentYear, CurrentDate = DateTime.Now.Date };

        var dashboardData = await _dapperRepository.QueryFirstOrDefaultAsync<MeterReadingDashboardDto>(sql, parameters);

        if (dashboardData == null)
        {
            return new ApiResponse<MeterReadingDashboardDto>(false, "Meter Readings dashboard data not found", default!);
        }

        return new ApiResponse<MeterReadingDashboardDto>(true, "Meter Readings dashboard data retrieved successfully", dashboardData);
    }
    #endregion

    #region EF-Core
    public async Task<ApiResponse<PaginationResponse<MeterReadingDetailDto>>> SearchAsync(MeterReadingFilterRequest request, CancellationToken cancellationToken)
    {
        var meterReadings = await _repository.SearchAsync(request, cancellationToken);

        // Map meterReadings to MeterReadingDetailDto
        var meterReadingDataList = meterReadings.ConvertAll(meterReading =>
        {
            return new MeterReadingDetailDto(
                FirstName: meterReading.Customer.FirstName,
                LastName: meterReading.Customer.LastName,
                AccountNumber: meterReading.Customer.AccountNumber,
                MeterNumber: meterReading?.Customer?.MeterNumber!,
                Tariff: meterReading.Customer.Tariff?.UniqueId!,
                PreviousReading: meterReading.PreviousReading,
                PresentReading: meterReading.PresentReading,
                Consumption: meterReading.PreviousReading - meterReading.PresentReading,
                DSSName: meterReading.DistributionTransformer?.Name!,
                MeterReadingType: meterReading.MeterReadingType.ToString(),
                IsApproved: meterReading.IsApproved,
                ImagePath: meterReading.ImagePath);
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<MeterReadingDetailDto>(
            meterReadingDataList,
            meterReadingDataList.Count,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<MeterReadingDetailDto>>(true, "Meter readings retrieved successfully", paginationResponse);
    }

    public async Task<ApiResponse<PaginationResponse<MeterReadingDetailDto>>> GetPendingReadinngsAsync(PaginationFilter paginationFilter, CancellationToken cancellationToken)
    {
        var meterReadings = await _repository.GetPendingReadinngsAsync(paginationFilter, cancellationToken);

        // Map meterReadings to MeterReadingDetailDto
        var meterReadingDataList = meterReadings.ConvertAll(meterReading =>
        {
            return new MeterReadingDetailDto(
                FirstName: meterReading.Customer.FirstName,
                LastName: meterReading.Customer.LastName,
                AccountNumber: meterReading.Customer.AccountNumber,
                MeterNumber: meterReading?.Customer?.MeterNumber!,
                Tariff: meterReading.Customer.Tariff?.UniqueId!,
                PreviousReading: meterReading.PreviousReading,
                PresentReading: meterReading.PresentReading,
                Consumption: meterReading.PreviousReading - meterReading.PresentReading,
                DSSName: meterReading.DistributionTransformer?.Name!,
                MeterReadingType: meterReading.MeterReadingType.ToString(),
                IsApproved: meterReading.IsApproved,
                ImagePath: meterReading.ImagePath);
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<MeterReadingDetailDto>(
            meterReadingDataList,
            meterReadingDataList.Count,
            paginationFilter.PageNumber,
            paginationFilter.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<MeterReadingDetailDto>>(true, "Pending MeterReadings retrieved successfully", paginationResponse);
    }
    #endregion
}
