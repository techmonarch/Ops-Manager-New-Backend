using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Downloads.Queries;
using OpsManagerAPI.Application.Features.Reconnections.Dtos;
using OpsManagerAPI.Application.Features.Reconnections.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;

public class ReconnectionRepository : IReconnectionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReconnectionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(List<Reconnection> Reconnections, int TotalCount)> GetPaginatedAsync(ReconnectionFilterRequest request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<Reconnection>(request);
        var filter = ReconnectionQueryBuilder.BuildFilter(request);

        int totalCount = await _dbContext.Reconnections
                         .AsNoTracking()
                         .Where(filter)
                         .CountAsync(cancellationToken: cancellationToken);

        var reconnections = await _dbContext.Reconnections
                                      .AsNoTracking()
                                      .Include(s => s.Customer)
                                      .ThenInclude(s => s.Tariff)
                                      .Include(s => s.Customer)
                                      .ThenInclude(s => s.DistributionTransformer)
                                      .WithSpecification(spec)
                                      .Where(filter)
                                      .ToListAsync(cancellationToken: cancellationToken);

        return (reconnections, totalCount);
    }

    public async Task<List<Reconnection>> GetReconnectionsByIdsAsync(List<DefaultIdType> reconnectionTaskIds, CancellationToken cancellationToken)
        => await _dbContext.Reconnections
            .Where(r => reconnectionTaskIds.Contains(r.Id))
            .ToListAsync(cancellationToken);

    public async Task UpdateReconnectionsAsync(List<Reconnection> reconnections, CancellationToken cancellationToken)
    {
        _dbContext.Reconnections.UpdateRange(reconnections);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
