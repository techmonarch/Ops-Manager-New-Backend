using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Disconnections.Commands;
public class CreateDisconnectionTicketCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType CustomerId { get; set; }
    public string? Reason { get; set; }
    public string? Comment { get; set; }
    public IFormFile? Image { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}

public class CreateDisconnectionTicketCommandHandler : IRequestHandler<CreateDisconnectionTicketCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Disconnection> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IFileStorageService _fileStorage;

    public CreateDisconnectionTicketCommandHandler(IRepositoryWithEvents<Disconnection> repository, ICurrentUser currentUser, IStaffRepository staffRepository, IFileStorageService fileStorage)
        => (_repository, _currentUser, _staffRepository, _fileStorage) = (repository, currentUser, staffRepository, fileStorage);
    public async Task<ApiResponse<DefaultIdType>> Handle(CreateDisconnectionTicketCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken);

        // Upload the image and create the new meter reading
        string imagePath = await _fileStorage.UploadAsync<string>(request.Image, FileType.Image, cancellationToken);

        var disconnection = new Disconnection(request.CustomerId, currentStaff.Id, 0, 0, request.Reason!, currentStaff.Id, imagePath, request.Comment, request.Latitude, request.Longitude);

        await _repository.AddAsync(disconnection, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Disconnection Ticket Successfully Created", disconnection.Id);
    }
}
