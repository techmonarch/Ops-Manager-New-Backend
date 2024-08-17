using OpsManagerAPI.Application.Features.Disconnections.Dtos;
using OpsManagerAPI.Application.Features.Reconnections.Dtos;

namespace OpsManagerAPI.Application.Features.Reconnections.Queries;
public interface IReconnectionQueries : ITransientService
{
    Task<ApiResponse<PaginationResponse<ReconnectionCustomerDetailsDto>>> GetAllReconnectionCustomers(ReconnectionFilterRequest request, CancellationToken cancellationToken);
}
