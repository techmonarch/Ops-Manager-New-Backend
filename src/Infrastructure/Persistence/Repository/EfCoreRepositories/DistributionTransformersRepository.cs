using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.DistributionTransformers.Dtos;
using OpsManagerAPI.Application.Features.DistributionTransformers.Queries;
using OpsManagerAPI.Application.Features.DistributionTransformers.Queries.QueryBuilder;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class DistributionTransformersRepository : IDistributionTransformersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DistributionTransformersRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<DistributionTransformerDetailDto>> GetAll(GetDistributionTransformersQueries request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = DistributionTransformerQueryBuilder.BuildFilter(request);

        var spec = new EntitiesByPaginationFilterSpec<DistributionTransformer>(request);

        return (await _dbContext.DistributionTransformers
                                                                .AsNoTracking()
                                                                .Where(filter)
                                                                .WithSpecification(spec)
                                                                .ToListAsync(cancellationToken))
                                                                .Adapt<List<DistributionTransformerDetailDto>>();
    }
}
