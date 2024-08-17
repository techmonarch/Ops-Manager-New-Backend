using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;

public enum MeterReadingType
{
    [Description(nameof(Customer))]
    Customer = 1,
    [Description(nameof(DistributionTransformer))]
    DistributionTransformer
}
