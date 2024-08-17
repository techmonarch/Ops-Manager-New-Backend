using OpsManagerAPI.Application.Features.Disconnections.Dtos;

using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Disconnections.Queries;
public interface IDisconnectionRepository : ITransientService
{
    Task<(List<Disconnection> Disconnections, int TotalCount)> GetPaginatedAsync(DisconnectionFilterRequest request, CancellationToken cancellationToken);

    Task<List<Disconnection>> GetDisconnectionsByIdsAsync(List<Guid> DisconnectionTaskIds, CancellationToken cancellationToken);
    Task UpdateDisconnectionsAsync(List<Disconnection> Disconnections, CancellationToken cancellationToken);
}
