using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Application.Features.Teams.Queries;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Reconnections.Commands;

public class CloseReconnectionTicketCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType ReconnectionTicketId { get; set; } = default!;
    public string? Comment { get; set; }
    public bool IsReconnected { get; set; }
    public IFormFile? Image { get; set; }
}

public class CloseReconnectionTicketCommandHandler : IRequestHandler<CloseReconnectionTicketCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Reconnection> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorageService _fileStorage;
    private readonly ITeamRepository _teamRepository;

    public CloseReconnectionTicketCommandHandler(IRepositoryWithEvents<Reconnection> repository, ICurrentUser currentUser, IStaffRepository staffRepository, IFileStorageService fileStorage, ITeamRepository teamRepository) => (_repository, _currentUser, _staffRepository, _fileStorage, _teamRepository) = (repository, currentUser, staffRepository, fileStorage, teamRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(CloseReconnectionTicketCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        var reconnectionTicket = await _repository.GetByIdAsync(request.ReconnectionTicketId, cancellationToken);

        if (!currentStaff.IsSuperAdmin)
        {
            var team = await _teamRepository.GetTeamByUserIdAsync(currentUserId, cancellationToken);

            if (team?.ReconnectionTaskIds.Contains(request.ReconnectionTicketId) != true)
            {
                throw new ForbiddenException("You do not have permission to close this reconnection ticket");
            }
        }

        // Upload the image and create the new meter reading
        string imagePath = await _fileStorage.UploadAsync<string>(request.Image, FileType.Image, cancellationToken);

        reconnectionTicket.Close(currentUserId, request.Comment, request.IsReconnected, imagePath);

        await _repository.UpdateAsync(reconnectionTicket, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Reconnection Ticket Successfully Closed", reconnectionTicket.Id);
    }
}