using OpsManagerAPI.Application.Features.Disconnections.Dtos;

namespace OpsManagerAPI.Application.Features.Disconnections.Queries;
public interface IDisconnectionQueries : ITransientService
{
    Task<ApiResponse<PaginationResponse<DisconnectionCustomersDetailDto>>> GetAllDisconnectionCustomers(DisconnectionFilterRequest request, CancellationToken cancellationToken);
}
