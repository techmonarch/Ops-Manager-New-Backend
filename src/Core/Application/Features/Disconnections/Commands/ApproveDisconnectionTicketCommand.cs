using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Disconnections.Commands;
public class ApproveDisconnectionTicketCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType DisconnectionTicketId { get; set; } = default!;
}

public class ApproveDisconnectionTicketCommandHandler : IRequestHandler<ApproveDisconnectionTicketCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Disconnection> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;

    public ApproveDisconnectionTicketCommandHandler(IRepositoryWithEvents<Disconnection> repository, ICurrentUser currentUser, IStaffRepository staffRepository) => (_repository, _currentUser, _staffRepository) = (repository, currentUser, staffRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(ApproveDisconnectionTicketCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        var disconnectionTicket = await _repository.GetByIdAsync(request.DisconnectionTicketId, cancellationToken);

        disconnectionTicket.Approve(currentStaff.Id);

        await _repository.UpdateAsync(disconnectionTicket, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Disconnection Ticket Successfully Approved", disconnectionTicket.Id);
    }
}