using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Reconnections.Commands;
public record ApproveReconnectionTicketCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType ReconnectionTicketId { get; set; } = default!;
}

public class ApproveReconnectionTicketCommandHandler : IRequestHandler<ApproveReconnectionTicketCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Reconnection> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;

    public ApproveReconnectionTicketCommandHandler(IRepositoryWithEvents<Reconnection> repository, ICurrentUser currentUser, IStaffRepository staffRepository) => (_repository, _currentUser, _staffRepository) = (repository, currentUser, staffRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(ApproveReconnectionTicketCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        var reconnectionTicket = await _repository.GetByIdAsync(request.ReconnectionTicketId, cancellationToken);

        reconnectionTicket.Approve(currentStaff.Id);

        await _repository.UpdateAsync(reconnectionTicket, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Reconnection Ticket Successfully Approved", reconnectionTicket.Id);
    }
}