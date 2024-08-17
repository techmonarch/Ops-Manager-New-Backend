namespace OpsManagerAPI.Application.Features.WebDashboards.Queries;

public class MainDashboardDto
{
    public int TotalCustomer { get; set; }
    public decimal LastMonthTotalBill { get; set; }
    public decimal LastMonthTotalCollection { get; set; }
    public decimal ThisMonthTotalCollection { get; set; }
    public int PrepaidCustomers { get; set; }
    public int PostpaidCustomers { get; set; }
    public int MDCustomers { get; set; }
    public int NMDCustomers { get; set; }
    public int ActiveCustomers { get; set; }
    public int SuspendedCustomers { get; set; }
    public int InactiveCustomers { get; set; }
    public int TotalDSS { get; set; }
}
