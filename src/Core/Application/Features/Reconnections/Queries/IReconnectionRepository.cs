using OpsManagerAPI.Application.Features.Reconnections.Dtos;
using OpsManagerAPI.Application.Features.Reconnections.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Downloads.Queries;
public interface IReconnectionRepository : ITransientService
{
    Task<(List<Reconnection> Reconnections, int TotalCount)> GetPaginatedAsync(ReconnectionFilterRequest request, CancellationToken cancellationToken);

    Task<List<Reconnection>> GetReconnectionsByIdsAsync(List<Guid> reconnectionTaskIds, CancellationToken cancellationToken);
    Task UpdateReconnectionsAsync(List<Reconnection> reconnections, CancellationToken cancellationToken);
}
