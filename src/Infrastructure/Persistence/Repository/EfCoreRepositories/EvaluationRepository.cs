using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Evaluations.Dtos;
using OpsManagerAPI.Application.Features.Evaluations.Queries;
using OpsManagerAPI.Application.Features.Evaluations.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class EvaluationRepository : IEvaluationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EvaluationRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Evaluation>> SearchAsync(EvaluationFilterRequest request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = EvaluationQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<Evaluation>(request);

        return await _dbContext.Evaluations
                                        .AsNoTracking()
                                        .WithSpecification(spec)
                                        .Where(filter)
                                        .ToListAsync(cancellationToken: cancellationToken);
    }
}
