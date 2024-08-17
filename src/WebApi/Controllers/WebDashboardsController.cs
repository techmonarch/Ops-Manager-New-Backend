using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.WebDashboards.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class WebDashboardsController : VersionNeutralApiController
{
    private readonly IWebDashboardQueries _webDashboardQueries;

    public WebDashboardsController(IWebDashboardQueries webDashboardQueries)
       => _webDashboardQueries = webDashboardQueries;

    [HttpGet("main")]
    [MustHavePermission(OPSAction.View, OPSResource.WebDashboards)]
    [OpenApiOperation("View Main Dashboard for the web", "Retrieve statistical details for the main dashboard of the web.")]
    public async Task<ApiResponse<MainDashboardDto>> MainDashboard()
        => await _webDashboardQueries.MainDashboard();

    [HttpGet("reconnection")]
    [MustHavePermission(OPSAction.View, OPSResource.WebDashboards)]
    [OpenApiOperation("View Reconnection Dashboard", "Retrieve statistical details for reconnection dashboard.")]
    public async Task<ApiResponse<List<ReconnectionDashboardDto>>> ReconnectionDashboard()
        => await _webDashboardQueries.ReconnectionDashboard();

    [HttpGet("disconnection")]
    [MustHavePermission(OPSAction.View, OPSResource.WebDashboards)]
    [OpenApiOperation("View Disconnection Dashboard", "Retrieve statistical details for disconnection dashboard.")]
    public async Task<ApiResponse<List<DisconnectionDashboardDto>>> DisconnectionDashboard()
        => await _webDashboardQueries.DisconnectionDashboard();

    [HttpGet("meter-reading")]
    [MustHavePermission(OPSAction.View, OPSResource.WebDashboards)]
    [OpenApiOperation("View Meter Reading Dashboard", "Retrieve statistical details for meter reading dashboard.")]
    public async Task<ApiResponse<List<MeterReadingDashboardDto>>> MeterReadingDashboard()
        => await _webDashboardQueries.MeterReadingDashboard();

    [HttpGet("enumeration")]
    [MustHavePermission(OPSAction.View, OPSResource.WebDashboards)]
    [OpenApiOperation("View Enumeration Dashboard", "Retrieve statistical details for enumeration dashboard.")]
    public async Task<ApiResponse<List<EnumerationDashboardDto>>> EnumerationDashboard()
        => await _webDashboardQueries.EnumerationDashboard();
}
