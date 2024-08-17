using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Complaints.Commands;

public class UpdateComplaintStatusCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public Guid ComplaintId { get; set; }
    public ComplaintStatus NewStatus { get; set; }
}

public class UpdateComplaintStatusCommandHandler : IRequestHandler<UpdateComplaintStatusCommand, ApiResponse<DefaultIdType>>
{
    public async Task<ApiResponse<DefaultIdType>> Handle(UpdateComplaintStatusCommand request, CancellationToken cancellationToken)
    {
        // Fetch complaint from database
        // var complaint = /* fetch from DB */;

        // complaint.Status = request.NewStatus;
        // complaint.UpdatedDate = DateTime.UtcNow;

        // Save changes to database

        return await Task.FromResult(new ApiResponse<DefaultIdType>(true, string.Empty, DefaultIdType.NewGuid()));
    }
}
