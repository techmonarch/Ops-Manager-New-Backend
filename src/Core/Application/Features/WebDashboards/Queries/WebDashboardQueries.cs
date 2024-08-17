using OpsManagerAPI.Application.Features.BillDistributions.Dtos;

namespace OpsManagerAPI.Application.Features.WebDashboards.Queries;
public class WebDashboardQueries : IWebDashboardQueries
{
    #region Constructor
    // private readonly IDapperRepository _dapperRepository;
    // public WebDashboardQueries(IDapperRepository dapperRepository)
    // {
    //     _dapperRepository = dapperRepository;
    // }
    #endregion
    public Task<ApiResponse<MainDashboardDto>> MainDashboard()
    {
        // int currentMonth = DateTime.Now.Month;
        // int currentYear = DateTime.Now.Year;
        // const string sql = @"
        //                     SELECT
        //                         (SELECT COUNT(DISTINCT c.Id)
        //                             FROM [Customers] c
        //                             WHERE c.AccountType = 2) AS PostPaidCustomersCount,
        //                         (SELECT COUNT(*)
        //                             FROM [BillDistributions] bd
        //                             WHERE MONTH(bd.DistributionDate) = @CurrentMonth AND YEAR(bd.DistributionDate) = @CurrentYear) AS DistributedBillsCount,
        //                         (SELECT COUNT(*)
        //                             FROM [BillDistributions] bd
        //                             WHERE MONTH(bd.DistributionDate) = @CurrentMonth AND YEAR(bd.DistributionDate) = @CurrentYear AND bd.IsDelivered = 0) AS PendingBillsCount,
        //                         (SELECT COUNT(*)
        //                             FROM [BillDistributions] bd
        //                             WHERE CONVERT(DATE, bd.DistributionDate) = @CurrentDate) AS TodayCount";

        // var parameters = new { CurrentMonth = currentMonth, CurrentYear = currentYear, CurrentDate = DateTime.Now.Date };

        // var dashboardData = await _dapperRepository.QueryFirstOrDefaultAsync<MainDashboardDto>(sql, parameters);

        // if (dashboardData == null)
        // {
        //     return new ApiResponse<MainDashboardDto>(false, "Bill distributions dashboard data not found", default!);
        // }

        return Task.FromResult(new ApiResponse<MainDashboardDto>(true, "Main Dashboard retrieved successfully", new()));
    }

    public Task<ApiResponse<List<DisconnectionDashboardDto>>> DisconnectionDashboard()
    {
        return Task.FromResult(new ApiResponse<List<DisconnectionDashboardDto>>(true, "Disconnection Dashboard retrieved successfully", new()));
    }

    public Task<ApiResponse<List<EnumerationDashboardDto>>> EnumerationDashboard()
    {
        return Task.FromResult(new ApiResponse<List<EnumerationDashboardDto>>(true, "Enumeration Dashboard retrieved successfully", new()));
    }

    public Task<ApiResponse<List<MeterReadingDashboardDto>>> MeterReadingDashboard()
    {
        return Task.FromResult(new ApiResponse<List<MeterReadingDashboardDto>>(true, "Meter reading retrieved successfully", new()));
    }

    public Task<ApiResponse<List<ReconnectionDashboardDto>>> ReconnectionDashboard()
    {
        return Task.FromResult(new ApiResponse<List<ReconnectionDashboardDto>>(true, "Resconnection Dashboard retrieved successfully", new()));
    }
}
