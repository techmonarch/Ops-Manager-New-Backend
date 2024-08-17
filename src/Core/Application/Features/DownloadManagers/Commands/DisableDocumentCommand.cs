using OpsManagerAPI.Domain.Aggregates.DownloadsManager;

namespace OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

public record DisableDocumentCommand(DefaultIdType DocumentId) : IRequest<ApiResponse<DefaultIdType>>;

public class DisableDocumentCommandHandler : IRequestHandler<DisableDocumentCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<DownloadManager> _repository;

    public DisableDocumentCommandHandler(IRepositoryWithEvents<DownloadManager> repository) => _repository = repository;

    public async Task<ApiResponse<DefaultIdType>> Handle(DisableDocumentCommand request, CancellationToken cancellationToken)
    {
        var downloadManager = await _repository.GetByIdAsync(request.DocumentId, cancellationToken);

        downloadManager.Disable();

        await _repository.UpdateAsync(downloadManager, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Document disabled Successfully", downloadManager.Id);
    }
}
