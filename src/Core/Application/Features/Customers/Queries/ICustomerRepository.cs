using OpsManagerAPI.Application.Features.Customers.Dtos;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

namespace OpsManagerAPI.Application.Features.Customers.Queries;

public interface ICustomerRepository : ITransientService
{
    Task<(List<Customer> Customers, int Count)> SearchAsync(CustomerFilterRequest request, CancellationToken cancellationToken);
    Task<(List<Customer> Customers, int Count)> GetForMeterReadingsAndBillDistributions(CustomerFilterRequest request, CancellationToken cancellationToken);
    Task<(List<Customer> Customers, int Count)> GetForEvaluationsAndEnumerations(CustomerFilterRequest request, CancellationToken cancellationToken);
    Task<Customer> GetById(Guid userId, CancellationToken cancellationToken);
}
