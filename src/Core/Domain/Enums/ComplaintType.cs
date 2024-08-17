using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;
public enum ComplaintType
{
    [Description(nameof(Customer))]
    Customer = 1,
    [Description(nameof(DistributionTransformer))]
    DistributionTransformer
}
