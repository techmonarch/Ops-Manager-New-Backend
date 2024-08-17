using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.MeterReadings.Specifications;

public interface IMeterReadingSpecification : ISpecification<MeterReading>
{
    // Add any common methods or properties if needed
}

public class MeterReadingByCustomerIdAndDateRangeSpec : Specification<MeterReading>, IMeterReadingSpecification
{
    public MeterReadingByCustomerIdAndDateRangeSpec(Guid? customerId, DateTime startDate, DateTime endDate)
    {
        Query.Where(mr => mr.CustomerId == customerId && mr.ReadingDate >= startDate && mr.ReadingDate <= endDate);
    }
}

public class MeterReadingByDistributionTransformerIdAndDateRangeSpec : Specification<MeterReading>, IMeterReadingSpecification
{
    public MeterReadingByDistributionTransformerIdAndDateRangeSpec(Guid? transformerId, DateTime startDate, DateTime endDate)
    {
        Query.Where(mr => mr.DistributionTransformerId == transformerId && mr.ReadingDate >= startDate && mr.ReadingDate <= endDate);
    }
}
