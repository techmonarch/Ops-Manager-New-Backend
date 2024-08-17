using OpsManagerAPI.Application.Features.BillDistributions.Dtos;

namespace OpsManagerAPI.Application.Features.BillDistributions.Queries;
public interface IBillDistributionQueries : ITransientService
{
    Task<ApiResponse<PaginationResponse<BillDistributionDetailDto>>> SearchAsync(BillDistributionFilterRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<BillDistributionDashboardDto>> Dashboard();
}
