using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Domain.Aggregates.DownloadsManager;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

public class UploadDocumentCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public IFormFile File { get; set; } = default!;
    public List<string> Roles { get; set; } = new();
    public bool IsEnabled { get; set; }
}

public class UploadDocumentCommandHandler : IRequestHandler<UploadDocumentCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<DownloadManager> _repository;
    private readonly IFileStorageService _fileStorageService;

    public UploadDocumentCommandHandler(IRepositoryWithEvents<DownloadManager> repository, IFileStorageService fileStorageService)
        => (_repository, _fileStorageService) = (repository, fileStorageService);

    public async Task<ApiResponse<DefaultIdType>> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        string imagePath = await _fileStorageService.UploadAsync<string>(request.File, FileType.Image, cancellationToken);

        var downloadManager = new DownloadManager(request.Title, request.Description, imagePath, request.Roles, request.IsEnabled);

        await _repository.AddAsync(downloadManager, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Document Uploaded Successfully", downloadManager.Id);
    }
}