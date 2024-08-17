using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Application.Features.Teams.Queries;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Disconnections.Commands;

public class CloseDisconnectionTicketCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType DisconnectionTicketId { get; set; } = default!;
    public string? Comment { get; set; }
    public bool IsDisconnected { get; set; }
    public IFormFile? Image { get; set; }
}

public class CloseDisconnectionTicketCommandHandler : IRequestHandler<CloseDisconnectionTicketCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Disconnection> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorageService _fileStorage;
    private readonly ITeamRepository _teamRepository;

    public CloseDisconnectionTicketCommandHandler(
        IRepositoryWithEvents<Disconnection> repository,
        ICurrentUser currentUser,
        IStaffRepository staffRepository,
        IFileStorageService fileStorage,
        ITeamRepository teamRepository) =>
        (_repository, _currentUser, _staffRepository, _fileStorage, _teamRepository) =
        (repository, currentUser, staffRepository, fileStorage, teamRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(CloseDisconnectionTicketCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken) ?? throw new InvalidOperationException("Error getting loggedin user's profile");

        var disconnectionTicket = await _repository.GetByIdAsync(request.DisconnectionTicketId, cancellationToken);

        if (!currentStaff.IsSuperAdmin)
        {
            var team = await _teamRepository.GetTeamByUserIdAsync(currentUserId, cancellationToken);

            if (team?.DisconnectionTaskIds.Contains(request.DisconnectionTicketId) != true)
            {
                throw new ForbiddenException("You do not have permission to close this disconnection ticket");
            }
        }

        // Upload the image and create the new meter reading
        string imagePath = await _fileStorage.UploadAsync<string>(request.Image, FileType.Image, cancellationToken);

        disconnectionTicket.Close(currentUserId, request.Comment, request.IsDisconnected, imagePath);

        await _repository.UpdateAsync(disconnectionTicket, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Disconnection Ticket Successfully Closed", disconnectionTicket.Id);
    }
}