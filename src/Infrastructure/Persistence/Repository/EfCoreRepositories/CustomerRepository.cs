using Microsoft.EntityFrameworkCore;
using Ardalis.Specification.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Application.Features.Customers.Dtos;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Application.Features.Customers.Queries;
using OpsManagerAPI.Application.Features.Customers.Queries.QueryBuilders;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Customer> GetById(DefaultIdType customerId, CancellationToken cancellationToken)
     => (await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken))!;

    public async Task<(List<Customer> Customers, int Count)> GetForMeterReadingsAndBillDistributions(CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = CustomerQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<Customer>(request);

        // Calculate the total count of customers matching the filter
        int totalCount = await _dbContext.Customers
                                         .AsNoTracking()
                                         .Where(filter)
                                         .CountAsync(cancellationToken: cancellationToken);

        // Fetch the paginated customers including the related entities
        var customers = await _dbContext.Customers
                                         .AsNoTracking()
                                         .Include(c => c.Tariff)
                                         .Include(c => c.MeterReadings)
                                         .Include(c => c.BillDistributions)
                                         .Include(x => x.DistributionTransformer)
                                         .WithSpecification(spec)
                                         .Where(filter)
                                         .ToListAsync(cancellationToken: cancellationToken);

        return (customers, totalCount);
    }

    public async Task<(List<Customer> Customers, int Count)> SearchAsync(CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = CustomerQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<Customer>(request);

        // Calculate the total count of customers matching the filter
        int totalCount = await _dbContext.Customers
                                         .AsNoTracking()
                                         .Where(filter)
                                         .CountAsync(cancellationToken: cancellationToken);

        // Fetch the paginated customers including the related entities
        var customers = await _dbContext.Customers
                                         .AsNoTracking()
                                         .Include(c => c.Tariff)
                                         .Include(x => x.DistributionTransformer)
                                         .WithSpecification(spec)
                                         .Where(filter)
                                         .ToListAsync(cancellationToken: cancellationToken);

        return (customers, totalCount);
    }

    public async Task<(List<Customer> Customers, int Count)> GetForEvaluationsAndEnumerations(CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var filter = CustomerQueryBuilder.BuildFilter(request);
        var spec = new EntitiesByPaginationFilterSpec<Customer>(request);

        // Calculate the total count of customers matching the filter
        int totalCount = await _dbContext.Customers
                                         .AsNoTracking()
                                         .Where(filter)
                                         .CountAsync(cancellationToken: cancellationToken);

        // Fetch the paginated customers including the related entities
        var customers = await _dbContext.Customers
                                         .AsNoTracking()
                                         .Include(c => c.Tariff)
                                         .Include(c => c.Evaluations)
                                         .Include(c => c.Enumerations)
                                         .Include(x => x.DistributionTransformer)
                                         .WithSpecification(spec)
                                         .Where(filter)
                                         .ToListAsync(cancellationToken: cancellationToken);

        return (customers, totalCount);
    }
}