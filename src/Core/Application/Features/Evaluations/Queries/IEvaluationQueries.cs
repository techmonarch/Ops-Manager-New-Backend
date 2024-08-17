using OpsManagerAPI.Application.Features.Evaluations.Dtos;

namespace OpsManagerAPI.Application.Features.Evaluations.Queries;
public interface IEvaluationQueries : ITransientService
{
    Task<ApiResponse<PaginationResponse<EvaluationDetailDto>>> SearchAsync(EvaluationFilterRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<EvaluationDashboardDto>> Dashboard();
}
