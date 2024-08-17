namespace OpsManagerAPI.Application.Features.BillDistributions.Dtos;

public record BillDistributionDashboardDto(
    int PostPaidCustomersCount,
    int DistributedBillsCount,
    int PendingBillsCount,
    int TodayCount
)
{
    public BillDistributionDashboardDto()
        : this(0, 0, 0, 0)
    {
    }
}
