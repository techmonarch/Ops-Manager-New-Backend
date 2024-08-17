using OpsManagerAPI.Application.Features.Enumerations.Dtos;

namespace OpsManagerAPI.Application.Features.Enumerations.Queries;
public interface IEnumerationsQueries : ITransientService
{
    Task<ApiResponse<PaginationResponse<EnumerationDetailDto>>> SearchAsync(EnumerationFilterRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<EnumerationDashboardDto>> Dashboard();
}
