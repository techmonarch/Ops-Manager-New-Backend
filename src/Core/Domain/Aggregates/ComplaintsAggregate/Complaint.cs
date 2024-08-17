using OpsManagerAPI.Domain.Enums;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Exceptions;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

namespace OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
public class Complaint : AuditableEntity, IAggregateRoot
{
    public DefaultIdType CategoryId { get; private set; }
    public DefaultIdType SubCategoryId { get; private set; }
    public string? Comment { get; private set; }
    public string? ImagePath { get; private set; }
    public ComplaintStatus Status { get; private set; }
    public DefaultIdType? CustomerId { get; private set; }
    public DefaultIdType? DistributionTransformerId { get; private set; }

    #region Relationships
    public Customer? Customer { get; private set; }
    public ComplaintCategory? Category { get; private set; }
    public ComplaintSubCategory? SubCategory { get; private set; }
    public DistributionTransformer? DistributionTransformer { get; set; }
    #endregion

    #region Constructors
    private Complaint()
    {
    }

    public Complaint(DefaultIdType categoryId, DefaultIdType subCategoryId, string comment, string? imagePath, DefaultIdType? customerId, DefaultIdType? distributionTransformerId)
    {
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        Comment = comment;
        ImagePath = imagePath;
        Status = ComplaintStatus.Opened;
        CustomerId = customerId;
        DistributionTransformerId = distributionTransformerId;
    }

    #endregion

    #region Behaviours
    public void Assign()
    {
        if (Status != ComplaintStatus.Opened && Status != ComplaintStatus.InfoNeeded)
        {
            throw new UserMessageDomainException("Cannot assign a dispute that is not opened.");
        }
    }

    public void StaffComment(string staffName, string comment)
    {
        if (string.IsNullOrWhiteSpace(staffName)) throw new ArgumentException($"'{nameof(staffName)}' cannot be null or whitespace.", nameof(staffName));
        if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException($"'{nameof(comment)}' cannot be null or whitespace.", nameof(comment));
        SetInfoNeededStatus();

        // _comments.Add(new DisputeComment(staffName, comment, Id));
    }

    public void AddCustomerComment(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException($"'{nameof(comment)}' cannot be null or whitespace.", nameof(comment));
        SetOpenStatus();

        // _comments.Add(new DisputeComment(CustomerId, comment, Id));
    }

    public void Resolve()
    {
        SetResolvedStatus();

        // _resolution = new DisputeResolution(resolvedByName, actionTaken, Id);
        SetClosedStatus();
    }

    public void SetClosedStatus()
    {
        // prevent error while changing to same status
        if (Status == ComplaintStatus.Closed) return;

        if (!Status.CanTransitionTo(ComplaintStatus.Closed))
        {
            StatusChangeException(ComplaintStatus.Closed);
        }
    }

    private void SetOpenStatus()
    {
        // prevent error while changing to same status
        if (Status == ComplaintStatus.Opened) return;

        if (!Status.CanTransitionTo(ComplaintStatus.Opened))
        {
            StatusChangeException(ComplaintStatus.Opened);
        }
    }

    private void SetInfoNeededStatus()
    {
        // prevent error while changing to same status
        if (Status == ComplaintStatus.InfoNeeded) return;

        if (!Status.CanTransitionTo(ComplaintStatus.InfoNeeded))
        {
            StatusChangeException(ComplaintStatus.InfoNeeded);
        }
    }

    private void SetResolvedStatus()
    {
        const ComplaintStatus statusToSet = ComplaintStatus.Resolved;

        // prevent error while changing to same status
        if (Status == statusToSet) return;

        if (!Status.CanTransitionTo(statusToSet))
        {
            StatusChangeException(statusToSet);
        }
    }

    private void StatusChangeException(ComplaintStatus statusToChangeTo)
    {
        throw new UserMessageDomainException($"Is not possible to change the dispute status from {Status} to {statusToChangeTo}.");
    }
    #endregion
}
