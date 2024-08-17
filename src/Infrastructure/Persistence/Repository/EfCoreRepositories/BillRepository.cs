using Mapster;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Billings.Dtos;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Application.Features.Billings.Queries;
using OpsManagerAPI.Application.Features.Billings.Queries.QueryBuilders;
using Ardalis.Specification.EntityFrameworkCore;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class BillRepository : IBillingRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BillRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<(List<BillingDetailsDto> Bills, int TotalCount)> GetAll(GetBillingQueries request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<Billing>(request);
        var filter = BillingQueryBuilder.BuildFilter(request);

        int totalCount = await _dbContext.Billings
                         .AsNoTracking()
                         .Where(filter)
                         .CountAsync(cancellationToken: cancellationToken);

        var billingPagination = _dbContext.Billings
                                  .AsNoTracking()
                                  .Where(filter);

        if (request.StartDate != null && request.EndDate != null)
        {
            billingPagination = billingPagination
                .Where(s => s.BillDate >= request.StartDate && s.BillDate <= request.EndDate);
        }
        else
        {
            DateTime sixMonthsBills = DateTime.UtcNow.AddMonths(-6);
            billingPagination = billingPagination
                .Where(s => s.BillDate >= sixMonthsBills);
        }

        var billings = (await billingPagination
            .WithSpecification(spec)
            .OrderByDescending(s => s.BillDate)
            .ToListAsync(cancellationToken: cancellationToken))
            .Adapt<List<BillingDetailsDto>>();

        return (billings, totalCount);
    }
}
