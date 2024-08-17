using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.BillDistributions.Dtos;
using OpsManagerAPI.Application.Features.BillDistributions.Queries;
using OpsManagerAPI.Application.Features.BillDistributions.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class BilDistributionRepository : IBillDistributionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BilDistributionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BillDistribution>> SearchAsync(BillDistributionFilterRequest request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = BillDistributionQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<BillDistribution>(request);

        return await _dbContext.BillDistributions
                                             .AsNoTracking()
                                             .Include(bd => bd.Customer)
                                                 .ThenInclude(c => c.DistributionTransformer)
                                             .Include(bd => bd.Customer)
                                                 .ThenInclude(c => c.Tariff)
                                             .WithSpecification(spec)
                                             .Where(filter)
                                             .ToListAsync(cancellationToken: cancellationToken);
    }
}
