using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.MeterReadings.Dtos;
using OpsManagerAPI.Application.Features.MeterReadings.Queries;
using OpsManagerAPI.Application.Features.MeterReadings.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class MeterReadingRepository : IMeterReadingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MeterReadingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MeterReading>> GetPendingReadinngsAsync(PaginationFilter filter, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<MeterReading>(filter);

        return await _dbContext.MeterReadings
                                        .AsNoTracking()
                                        .Include(c => c.Customer)
                                        .Include(c => c.DistributionTransformer)
                                        .WithSpecification(spec)
                                        .Where(x => !x.IsApproved)
                                        .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<MeterReading>> SearchAsync(MeterReadingFilterRequest request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = MeterReadingQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<MeterReading>(request);

        return await _dbContext.MeterReadings
                                        .AsNoTracking()
                                        .Include(c => c.Customer)
                                        .ThenInclude(c => c.Tariff)
                                        .Include(c => c.DistributionTransformer)
                                        .WithSpecification(spec)
                                        .Where(filter)
                                        .ToListAsync(cancellationToken: cancellationToken);
    }
}
