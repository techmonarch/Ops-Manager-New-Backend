using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;
using OpsManagerAPI.Domain.Exceptions;

namespace OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
public class Disconnection : AuditableEntity, IAggregateRoot
{
    public DefaultIdType CustomerId { get; private set; }
    public DefaultIdType StaffId { get; private set; }
    public decimal AmountOwed { get; private set; }
    public DateTime DateLogged { get; private set; }
    public decimal AmountToPay { get; private set; }
    public string? Reason { get; private set; }
    public string? Comment { get; private set; }
    public DefaultIdType? ReportedBy { get; private set; }
    public DefaultIdType? ApprovedBy { get; private set; }
    public DisconnectionStatus Status { get; private set; }
    public DateTime? DateDisconnected { get; private set; }
    public DateTime? DateApproved { get; private set; }
    public DefaultIdType? DisconnectedBy { get; private set; }
    public string? ImagePath { get; private set; }
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }

    #region Relationships
    public Customer? Customer { get; private set; }
    public Staff? Staff { get; private set; }
    #endregion

    #region Constructors

    private Disconnection()
    {
    }

    public Disconnection(DefaultIdType customerId, DefaultIdType staffId, decimal amountOwed, decimal amountToPay, string reason, DefaultIdType? reportedBy, string? imagePath, string? comment, decimal latitude, decimal longitude)
    {
        CustomerId = customerId;
        AmountOwed = amountOwed;
        AmountToPay = amountToPay;
        Reason = reason;
        Comment = comment;
        StaffId = staffId;
        ReportedBy = reportedBy;
        Status = DisconnectionStatus.Pending;
        ImagePath = imagePath;
        DateLogged = DateTime.Now;
        Latitude = latitude;
        Longitude = longitude;
    }

    #endregion

    #region Behaviours
    public void Approve(DefaultIdType userId)
    {
        const DisconnectionStatus statusToSet = DisconnectionStatus.Approved;

        // prevent error while changing to same status
        if (Status == statusToSet) return;

        if (!Status.CanTransitionTo(statusToSet))
        {
            StatusChangeException(statusToSet);
        }

        Status = statusToSet;
        ApprovedBy = userId;
        DateApproved = DateTime.Now;
    }

    public void DisApprove(DefaultIdType userId)
    {
        const DisconnectionStatus statusToSet = DisconnectionStatus.Disapproved;

        // prevent error while changing to same status
        if (Status == statusToSet) return;

        if (!Status.CanTransitionTo(statusToSet))
        {
            StatusChangeException(statusToSet);
        }

        Status = statusToSet;
        ApprovedBy = userId;
        DateApproved = DateTime.Now;
    }

    public void Assign()
    {
        const DisconnectionStatus statusToSet = DisconnectionStatus.Assigned;

        // prevent error while changing to same status
        if (Status == statusToSet) return;

        if (!Status.CanTransitionTo(statusToSet))
        {
            StatusChangeException(statusToSet);
        }

        Status = statusToSet;
    }

    public void Close(DefaultIdType userId, string? comment, bool isDisconnected, string imagePath)
    {
        DisconnectionStatus statusToSet = isDisconnected ? DisconnectionStatus.Disconnected : DisconnectionStatus.NotDisconnected;

        // Prevent error while changing to the same status
        if (Status == statusToSet) return;

        if (!Status.CanTransitionTo(statusToSet))
        {
            StatusChangeException(statusToSet);
        }

        Status = statusToSet;
        Comment = comment!;
        DisconnectedBy = userId;
        DateDisconnected = DateTime.Now;
        ImagePath = imagePath;
    }

    private void StatusChangeException(DisconnectionStatus statusToChangeTo)
    {
        throw new InvalidOperationException($"Is not possible to change the disconnection status from {Status} to {statusToChangeTo}.");
    }
    #endregion
}
