namespace OpsManagerAPI.Application.Features.Enumerations.Dtos;

public record EnumerationDashboardDto(
    int TotalCustomersCount,
    int EnumeratedCustomersCount,
    int PendingEnumerationCount,
    int TodayCount
)
{
    public EnumerationDashboardDto()
        : this(0, 0, 0, 0)
    {
    }
}
