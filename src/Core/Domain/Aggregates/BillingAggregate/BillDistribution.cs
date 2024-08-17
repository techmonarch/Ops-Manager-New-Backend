using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.BillingAggregate;
public class BillDistribution : AuditableEntity, IAggregateRoot
{
    public DefaultIdType CustomerId { get; private set; }
    public DefaultIdType StaffId { get; private set; }
    public decimal BillAmount { get; private set; }
    public string Comment { get; private set; } = default!;
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }
    public DateTime? DistributionDate { get; private set; }
    public bool IsDelivered { get; private set; }

    #region Relationships
    public Customer? Customer { get; private set; }
    public Staff? Staff { get; private set; }
    #endregion

    #region Constructors
    public BillDistribution(DefaultIdType customerId, DefaultIdType staffId, decimal billAmount, decimal latitude, decimal longitude, DateTime? distributionDate, bool isDelivered, string? comment)
    {
        CustomerId = customerId;
        BillAmount = billAmount;
        Latitude = latitude;
        Longitude = longitude;
        StaffId = staffId;
        DistributionDate = distributionDate;
        IsDelivered = isDelivered;
        Comment = comment ?? " ";
    }

    public BillDistribution()
    {
    }
    #endregion

    #region Behaviours
    public void SetBillDelivered()
    {
        IsDelivered = true;
    }
    #endregion
}
