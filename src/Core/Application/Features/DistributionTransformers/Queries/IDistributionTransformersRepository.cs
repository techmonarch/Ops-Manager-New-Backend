using OpsManagerAPI.Application.Features.DistributionTransformers.Dtos;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.DistributionTransformers.Queries;
public interface IDistributionTransformersRepository : ITransientService
{
    Task<List<DistributionTransformerDetailDto>> GetAll(GetDistributionTransformersQueries request, CancellationToken cancellationToken);
}
