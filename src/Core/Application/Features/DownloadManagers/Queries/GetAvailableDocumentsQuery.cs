using OpsManagerAPI.Application.Features.DownloadsManagers.Dtos;

namespace OpsManagerAPI.Application.Features.DownloadsManagers.Queries;

public class GetAvailableDocumentsQuery : PaginationFilter, IRequest<ApiResponse<PaginationResponse<DownloadManagerDto>>>
{
    public DefaultIdType? Id { get; set; }
}

public class GetAvailableDocumentsQueryHandler : IRequestHandler<GetAvailableDocumentsQuery, ApiResponse<PaginationResponse<DownloadManagerDto>>>
{
    private readonly IDownloadManagerRepository _repository;

    public GetAvailableDocumentsQueryHandler(IDownloadManagerRepository repository) => _repository = repository;

    public async Task<ApiResponse<PaginationResponse<DownloadManagerDto>>> Handle(GetAvailableDocumentsQuery query, CancellationToken cancellationToken)
    {
        var (documents, totalCount) = await _repository.GetAllPaginatedAsync(query, cancellationToken);

        // Create the pagination response
        var paginationResponse = new PaginationResponse<DownloadManagerDto>(
            documents,
            totalCount,
            query.PageNumber,
            query.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<DownloadManagerDto>>(true, "Downloadable documents retrieved successfully", paginationResponse);
    }
}
