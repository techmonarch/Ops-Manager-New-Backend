using OpsManagerAPI.Application.Features.BillDistributions.Dtos;
using OpsManagerAPI.Application.Features.Customers.Dtos;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;

namespace OpsManagerAPI.Application.Features.BillDistributions.Queries;

public interface IBillDistributionRepository : ITransientService
{
    Task<List<BillDistribution>> SearchAsync(BillDistributionFilterRequest request, CancellationToken cancellationToken);
}
