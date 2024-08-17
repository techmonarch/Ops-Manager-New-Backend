using OpsManagerAPI.Application.Features.DownloadsManagers.Queries;
using OpsManagerAPI.Domain.Aggregates.DownloadsManager;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.DownloadManagers.Queries.QueryBuilder;
public static class DownloadManagerQueryBuilder
{
    public static Expression<Func<DownloadManager, bool>> BuildFilter(GetAvailableDocumentsQuery request)
    {
        return distributionTransformer =>
            request.Id == null || distributionTransformer.Id == request.Id;
    }
}
