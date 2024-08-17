using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Enumerations.Dtos;
using OpsManagerAPI.Application.Features.Enumerations.Queries;
using OpsManagerAPI.Application.Features.Enumerations.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class EnumerationRepository : IEnumerationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public EnumerationRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Enumeration>> SearchAsync(EnumerationFilterRequest request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = EnumerationQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<Enumeration>(request);

        return await _dbContext.Enumerations
                                        .AsNoTracking()
                                        .Include(e => e.Tariff)
                                        .WithSpecification(spec)
                                        .Where(filter)
                                        .ToListAsync(cancellationToken: cancellationToken);
    }
}
