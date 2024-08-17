using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Domain.Aggregates.MeterAggregate;
public class MeterReading : AuditableEntity, IAggregateRoot
{
    public DefaultIdType StaffId { get; private set; }
    public DefaultIdType? CustomerId { get; private set; }
    public DefaultIdType? DistributionTransformerId { get; private set; }
    public decimal PreviousReading { get; private set; }
    public decimal PresentReading { get; private set; }
    public decimal Consumption { get; private set; }
    public DateTime ReadingDate { get; private set; }
    public string? ImagePath { get; private set; }
    public string? Comment { get; private set; }
    public bool IsApproved { get; private set; }
    public bool IsMeterRead { get; private set; }
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }
    public MeterReadingType MeterReadingType { get; private set; }

    #region Relationships
    public virtual Customer? Customer { get; private set; }
    public virtual Staff? Staff { get; private set; }
    public virtual DistributionTransformer? DistributionTransformer { get; private set; }
    #endregion

    #region Constructors
    private MeterReading()
    {
    }

    public MeterReading(DefaultIdType staffId, decimal previousReading, decimal presentReading, decimal consumption, string? imagePath, decimal latitude, decimal longitude, string? comment, MeterReadingType meterReadingType, DefaultIdType? customerId, DefaultIdType? distributionTransformerId, bool isMeterRead)
    {
        CustomerId = customerId;
        StaffId = staffId;
        DistributionTransformerId = distributionTransformerId;
        PreviousReading = previousReading;
        PresentReading = presentReading;
        Consumption = consumption;
        ImagePath = imagePath;
        Latitude = latitude;
        Longitude = longitude;
        Comment = comment;
        IsApproved = false;
        MeterReadingType = meterReadingType;
        IsMeterRead = isMeterRead;
        ReadingDate = DateTime.Now;
    }
    #endregion

    #region Behaviours
    public void Approve(Guid userId)
    {
        if (IsApproved) return;

        IsApproved = true;
        LastModifiedBy = userId;
    }

    public void DisApprove(Guid userId)
    {
        if (!IsApproved) return;

        IsApproved = false;
        LastModifiedBy = userId;
    }

    #endregion
}
