using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Complaints.Commands;
public class CreateComplaintCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType CategoryId { get; set; }
    public DefaultIdType SubCategoryId { get; set; }
    public string? Comment { get; set; }
    public IFormFile? Image { get; set; }
    public DefaultIdType? CustomerId { get; set; }
    public DefaultIdType? DistributionTransformerId { get; set; }
}

public class CreateComplaintCommandHandler : IRequestHandler<CreateComplaintCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Complaint> _repository;
    private readonly IFileStorageService _fileStorage;

    public CreateComplaintCommandHandler(IRepositoryWithEvents<Complaint> repository, IFileStorageService fileStorage)
     => (_repository, _fileStorage) = (repository, fileStorage);

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateComplaintCommand request, CancellationToken cancellationToken)
    {
        string imagePath = await _fileStorage.UploadAsync<string>(request.Image, FileType.Image, cancellationToken);

        var complaints = new Complaint(request.CategoryId, request.SubCategoryId, request.Comment!, imagePath, request.CustomerId, request.DistributionTransformerId);

        await _repository.AddAsync(complaints, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Complaint Successfully Created", complaints.Id);
    }
}