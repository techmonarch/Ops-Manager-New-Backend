namespace OpsManagerAPI.Application.Features.WebDashboards.Queries;
public interface IWebDashboardQueries : ITransientService
{
    Task<ApiResponse<MainDashboardDto>> MainDashboard();
    Task<ApiResponse<List<DisconnectionDashboardDto>>> DisconnectionDashboard();
    Task<ApiResponse<List<ReconnectionDashboardDto>>> ReconnectionDashboard();
    Task<ApiResponse<List<MeterReadingDashboardDto>>> MeterReadingDashboard();
    Task<ApiResponse<List<EnumerationDashboardDto>>> EnumerationDashboard();
}
