using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
public class Reconnection : AuditableEntity, IAggregateRoot
{
    public DefaultIdType CustomerId { get; private set; }
    public DefaultIdType StaffId { get; private set; }
    public DateTime DateLogged { get; private set; }
    public string Reason { get; private set; } = default!;
    public string? Comment { get; private set; }
    public DefaultIdType ReportedBy { get; private set; }
    public DefaultIdType? ApprovedBy { get; private set; }
    public ReconnectionStatus Status { get; private set; }
    public DateTime DateReconnected { get; private set; }
    public DateTime DateApproved { get; private set; }
    public DefaultIdType? ReconnectedBy { get; private set; }
    public string? ImagePath { get; private set; }
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }

    #region Relationships
    public Customer? Customer { get; private set; }
    public Staff? Staff { get; private set; }
    #endregion

    #region Constructors

    public Reconnection(DefaultIdType customerId, DefaultIdType staffId, string reason, DefaultIdType reportedBy, string? imagePath, decimal latitude, decimal longitude, string comment)
    {
        CustomerId = customerId;
        DateLogged = DateTime.Now;
        Reason = reason;
        ReportedBy = reportedBy;
        StaffId = staffId;
        Status = ReconnectionStatus.Pending;
        ImagePath = imagePath;
        Latitude = latitude;
        Longitude = longitude;
        Comment = comment;
    }

    private Reconnection()
    {
    }
    #endregion

    #region Behaviours
    public void Approve(DefaultIdType userId)
    {
        const ReconnectionStatus statusToSet = ReconnectionStatus.Approved;

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
        const ReconnectionStatus statusToSet = ReconnectionStatus.Disapproved;

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
        const ReconnectionStatus statusToSet = ReconnectionStatus.Assigned;

        // prevent error while changing to same status
        if (Status == statusToSet) return;

        if (!Status.CanTransitionTo(statusToSet))
        {
            StatusChangeException(statusToSet);
        }

        Status = statusToSet;
    }

    public void Close(DefaultIdType userId, string? comment, bool isReconnected, string imagePath)
    {
        ReconnectionStatus statusToSet = isReconnected ? ReconnectionStatus.Reconnected : ReconnectionStatus.NotReconnected;

        // Prevent error while changing to the same status
        if (Status == statusToSet) return;

        if (!Status.CanTransitionTo(statusToSet))
        {
            StatusChangeException(statusToSet);
        }

        Status = statusToSet;
        Comment = comment!;
        ReconnectedBy = userId;
        DateReconnected = DateTime.Now;
        ImagePath = imagePath;
    }

    private void StatusChangeException(ReconnectionStatus statusToChangeTo)
    {
        throw new InvalidOperationException($"Is not possible to change the Reconnection status from {Status} to {statusToChangeTo}.");
    }
    #endregion
}
