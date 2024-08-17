using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

namespace OpsManagerAPI.Application.Features.Enumerations.Specifications;
public class EnumerationByAccountNumberSpec : Specification<Enumeration>
{
    public EnumerationByAccountNumberSpec(string accountNumber)
    {
        Query.Where(e => e.AccountNumber == accountNumber);
    }
}
