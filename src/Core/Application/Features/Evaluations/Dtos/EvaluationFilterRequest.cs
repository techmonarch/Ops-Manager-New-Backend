namespace OpsManagerAPI.Application.Features.Evaluations.Dtos;

public class EvaluationFilterRequest : PaginationFilter
{
    public string? CreatedBy { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
