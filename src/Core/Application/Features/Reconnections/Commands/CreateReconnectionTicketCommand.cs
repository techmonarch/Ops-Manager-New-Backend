using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Reconnections.Commands;
public class CreateReconnectionTicketCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType CustomerId { get; set; }
    public string? Reason { get; set; }
    public string? Comment { get; set; }
    public IFormFile? Image { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}

public class CreateReconnectionTicketCommandHandler : IRequestHandler<CreateReconnectionTicketCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Reconnection> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorageService _fileStorage;

    public CreateReconnectionTicketCommandHandler(IRepositoryWithEvents<Reconnection> repository, ICurrentUser currentUser, IStaffRepository staffRepository, IFileStorageService fileStorage) => (_repository, _currentUser, _staffRepository, _fileStorage) = (repository, currentUser, staffRepository, fileStorage);

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateReconnectionTicketCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        // Upload the image and create the new meter reading
        string imagePath = await _fileStorage.UploadAsync<string>(request.Image, FileType.Image, cancellationToken);

        var reconnection = new Reconnection(request.CustomerId, currentStaff.Id, request.Reason!, currentStaff.Id, imagePath, request.Latitude, request.Longitude, request.Comment!);

        await _repository.AddAsync(reconnection, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Reconnection Ticket Successfully Created", reconnection.Id);
    }
}