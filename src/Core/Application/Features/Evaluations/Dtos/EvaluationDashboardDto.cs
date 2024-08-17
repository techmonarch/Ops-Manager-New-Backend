namespace OpsManagerAPI.Application.Features.Evaluations.Dtos;

public record EvaluationDashboardDto(
    int TotalCustomersCount,
    int EvaluatedCustomersCount,
    int BypassCustomersCount,
    int PendingEvaluationCount,
    int TodayCount
)
{
    public EvaluationDashboardDto()
        : this(0, 0, 0, 0, 0)
    {
    }
}
