using OpsManagerAPI.Domain.Aggregates.DownloadsManager;

namespace OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

public record DeleteDocumentCommand(Guid DocumentId) : IRequest<ApiResponse<DefaultIdType>>;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<DownloadManager> _repository;

    public DeleteDocumentCommandHandler(IRepositoryWithEvents<DownloadManager> repository) => _repository = repository;

    public async Task<ApiResponse<DefaultIdType>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var downloadManager = await _repository.GetByIdAsync(request.DocumentId, cancellationToken) ?? throw new NotFoundException("Document not found");

        await _repository.DeleteAsync(downloadManager, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Document deleted Sucessfully", downloadManager.Id);
    }
}