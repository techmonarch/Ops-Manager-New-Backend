using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Domain.Aggregates.PaymentAggregate;
public class Payment : AuditableEntity, IAggregateRoot
{
    public DefaultIdType CustomerId { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public DefaultIdType BillId { get; private set; }
    public string? PaymentSource { get; private set; }
    public decimal Amount { get; private set; }
    public AccountType AccountType { get; private set; }
    public CustomerType CustomerType { get; private set; }
    #region Relationships
    public Customer? Customer { get; private set; }

    #endregion

    #region Constructor
    private Payment()
    {
    }

    public Payment(DefaultIdType customerId, DateTime paymentDate, DefaultIdType billId, string paymentSource, decimal amount, AccountType accountType, CustomerType customerType)
    {
        CustomerId = customerId;
        PaymentDate = paymentDate;
        BillId = billId;
        PaymentSource = paymentSource;
        Amount = amount;
        AccountType = accountType;
        CustomerType = customerType;
    }
    #endregion

}
