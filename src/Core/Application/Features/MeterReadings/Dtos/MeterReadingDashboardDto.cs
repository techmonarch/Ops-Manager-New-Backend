namespace OpsManagerAPI.Application.Features.MeterReadings.Dtos;

public record MeterReadingDashboardDto(
    int PostPaidCustomersCount,
    int MeterReadCount,
    int PendingReadingCount,
    int TodayCount
)
{
    public MeterReadingDashboardDto()
        : this(0, 0, 0, 0)
    {
    }
}
