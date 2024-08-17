using OpsManagerAPI.Domain.Aggregates.DownloadsManager;

namespace OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

public class UpdateDocumentAccessibilityCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public Guid DocumentId { get; set; }
    public List<string> Roles { get; set; } = new();
}

public class UpdateDocumentAccessibilityCommandHandler : IRequestHandler<UpdateDocumentAccessibilityCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<DownloadManager> _repository;

    public UpdateDocumentAccessibilityCommandHandler(IRepositoryWithEvents<DownloadManager> repository) => _repository = repository;

    public async Task<ApiResponse<DefaultIdType>> Handle(UpdateDocumentAccessibilityCommand request, CancellationToken cancellationToken)
    {
        var downloadManager = await _repository.GetByIdAsync(request.DocumentId, cancellationToken);

        downloadManager.UpdateAccessibility(request.Roles);

        await _repository.UpdateAsync(downloadManager, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Document Accessibility updated Sucessfully", downloadManager.Id);
    }
}
