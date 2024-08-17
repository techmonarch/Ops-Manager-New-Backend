using OpsManagerAPI.Application.Features.DownloadsManagers.Dtos;

namespace OpsManagerAPI.Application.Features.DownloadsManagers.Queries;

public interface IDownloadManagerRepository : IScopedService
{
    Task<(List<DownloadManagerDto> Documents, int Count)> GetAllPaginatedAsync(PaginationFilter filter, CancellationToken cancellationToken);
}
