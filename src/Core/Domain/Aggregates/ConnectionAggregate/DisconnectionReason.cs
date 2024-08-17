using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
public class DisconnectionReason : AuditableEntity, IAggregateRoot
{
    public int NumberOfMonth { get; private set; }
    public decimal Amount { get; private set; }
    public int PercentagePayment { get; private set; }

    #region Constructor

    private DisconnectionReason()
    {
    }

    public DisconnectionReason(int numberOfMonth, decimal amount, int percentagePayment)
    {
        NumberOfMonth = numberOfMonth;
        Amount = amount;
        PercentagePayment = percentagePayment;
    }

    #endregion
}
