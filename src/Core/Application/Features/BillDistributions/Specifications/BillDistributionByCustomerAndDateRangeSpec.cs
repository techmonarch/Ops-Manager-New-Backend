using OpsManagerAPI.Domain.Aggregates.BillingAggregate;

namespace OpsManagerAPI.Application.Features.BillDistributions.Specifications;
public class BillDistributionByCustomerAndDateRangeSpec : Specification<BillDistribution>
{
    public BillDistributionByCustomerAndDateRangeSpec(Guid customerId, DateTime startDate, DateTime endDate)
    {
        Query.Where(bd => bd.CustomerId == customerId && bd.DistributionDate >= startDate && bd.DistributionDate <= endDate);
    }
}
