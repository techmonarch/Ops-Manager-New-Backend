using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Disconnections.Dtos;
using OpsManagerAPI.Application.Features.Disconnections.Queries;
using OpsManagerAPI.Application.Features.Disconnections.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;

public class DisconnectionRepository : IDisconnectionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DisconnectionRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Disconnection>> GetDisconnectionsByIdsAsync(List<DefaultIdType> DisconnectionTaskIds, CancellationToken cancellationToken)
    {
        return await _dbContext.Disconnections
             .Where(d => DisconnectionTaskIds.Contains(d.Id))
              .ToListAsync(cancellationToken);
    }

    public async Task<(List<Disconnection> Disconnections, int TotalCount)> GetPaginatedAsync(DisconnectionFilterRequest request, CancellationToken cancellationToken)
    {
        var filter = DisconnectionQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<Disconnection>(request);

        int totalCount = await _dbContext.Disconnections
                         .AsNoTracking()
                         .Where(filter)
                         .CountAsync(cancellationToken: cancellationToken);

        var disconnections = await _dbContext.Disconnections
                                    .AsNoTracking()
                                    .Include(s => s.Customer)
                                    .ThenInclude(s => s.Tariff)
                                    .Include(s => s.Customer)
                                    .ThenInclude(s => s.DistributionTransformer)
                                    .Where(filter)
                                    .WithSpecification(spec)
                                    .ToListAsync(cancellationToken: cancellationToken);

        return (disconnections, totalCount);
    }

    public async Task UpdateDisconnectionsAsync(List<Disconnection> Disconnections, CancellationToken cancellationToken)
    {
        _dbContext.Disconnections.UpdateRange(Disconnections);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
