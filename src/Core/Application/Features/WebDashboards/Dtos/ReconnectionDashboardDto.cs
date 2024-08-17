namespace OpsManagerAPI.Application.Features.WebDashboards.Queries;

public class ReconnectionDashboardDto
{
    public string? RegionName { get; set; }
    public int Total { get; set; }
    public int LastMonth { get; set; }
    public int ThisMonth { get; set; }
    public int Pending { get; set; }
    public int Approved { get; set; }
    public int Denied { get; set; }
    public int Reconnected { get; set; }
}
