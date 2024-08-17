using OpsManagerAPI.Domain.Aggregates.DownloadsManager;

namespace OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

public record EnableDocumentCommand(DefaultIdType DocumentId) : IRequest<ApiResponse<DefaultIdType>>;

public class EnableDocumentCommandHandler : IRequestHandler<EnableDocumentCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<DownloadManager> _repository;

    public EnableDocumentCommandHandler(IRepositoryWithEvents<DownloadManager> repository) => _repository = repository;

    public async Task<ApiResponse<DefaultIdType>> Handle(EnableDocumentCommand request, CancellationToken cancellationToken)
    {
        var downloadManager = await _repository.GetByIdAsync(request.DocumentId, cancellationToken);

        downloadManager.Enable();

        await _repository.UpdateAsync(downloadManager, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Document Sucessfully Enabled", downloadManager.Id);
    }
}