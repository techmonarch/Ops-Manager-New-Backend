using OpsManagerAPI.Application.Features.Enumerations.Dtos;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

namespace OpsManagerAPI.Application.Features.Enumerations.Queries;

public interface IEnumerationRepository : ITransientService
{
    Task<List<Enumeration>> SearchAsync(EnumerationFilterRequest request, CancellationToken cancellationToken);
}
