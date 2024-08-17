using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.DownloadsManagers.Dtos;
using OpsManagerAPI.Application.Features.DownloadsManagers.Queries;
using OpsManagerAPI.Domain.Aggregates.DownloadsManager;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;

public class DownloadsManagerRepository : IDownloadManagerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DownloadsManagerRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<(List<DownloadManagerDto> Documents, int Count)> GetAllPaginatedAsync(PaginationFilter filter, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<DownloadManager>(filter);

        int totalCount = await _dbContext.Disconnections
                         .AsNoTracking()
                         .CountAsync(cancellationToken: cancellationToken);

        var documents = (await _dbContext.DownloadManagers
                                    .AsNoTracking()
                                    .WithSpecification(spec)
                                    .ToListAsync(cancellationToken: cancellationToken)).
                                    Adapt<List<DownloadManagerDto>>();

        return (documents, totalCount);
    }
}
