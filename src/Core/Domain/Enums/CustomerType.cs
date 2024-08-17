using System.ComponentModel;

namespace OpsManagerAPI.Domain.Enums;
public enum CustomerType
{
    [Description(nameof(MD1))]
    MD1 = 1, // MaximumDemand2
    [Description(nameof(MD2))]
    MD2, // MaximumDemand2
    [Description(nameof(NMD))]
    NMD // NonMaximumDemand
}
