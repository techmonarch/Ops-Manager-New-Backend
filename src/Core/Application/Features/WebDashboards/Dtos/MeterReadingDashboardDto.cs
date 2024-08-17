namespace OpsManagerAPI.Application.Features.WebDashboards.Queries;

public class MeterReadingDashboardDto
{
    public string? RegionName { get; set; }
    public int TotalPostpaidCustomer { get; set; }
    public int TotalMeteredCustomer { get; set; }
    public int TotalMeteredRead { get; set; }
    public int TotalMeteredReadToday { get; set; }
    public double MeterReadPercentage { get; set; }
    public int TotalReadingRemaining { get; set; }
}
