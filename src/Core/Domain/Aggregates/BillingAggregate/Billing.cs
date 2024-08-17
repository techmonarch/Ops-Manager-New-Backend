using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Domain.Aggregates.BillingAggregate;
public class Billing : AuditableEntity, IAggregateRoot
{
    public DefaultIdType CustomerId { get; private set; }
    public DateTime BillDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public decimal Consumption { get; private set; }
    public decimal Arrears { get; private set; }
    public decimal VAT { get; private set; }
    public decimal CurrentCharge { get; private set; }
    public decimal TotalCharge { get; private set; }
    public decimal TotalDue { get; private set; }
    public AccountType AccountType { get; private set; }
    public CustomerType CustomerType { get; private set; }

    #region Relationships

    public Customer? Customer { get; private set; }

    #endregion

    #region Constructors
    private Billing()
    {
    }

    public Billing(DateTime billDate, DateTime dueDate, decimal consumption, decimal arrears, decimal vat, decimal currentCharge, decimal totalCharge, decimal totalDue, DefaultIdType customerId)
    {
        BillDate = billDate;
        DueDate = dueDate;
        Consumption = consumption;
        Arrears = arrears;
        VAT = vat;
        CurrentCharge = currentCharge;
        TotalCharge = totalCharge;
        TotalDue = totalDue;
        CustomerId = customerId;
    }
    #endregion
}
