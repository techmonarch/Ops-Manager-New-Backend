using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.DistributionTransformers.Queries.QueryBuilder;
public static class DistributionTransformerQueryBuilder
{
    public static Expression<Func<DistributionTransformer, bool>> BuildFilter(GetDistributionTransformersQueries request)
    {
        return distributionTransformer =>
            (request.Id == null || distributionTransformer.Id == request.Id) &&
            (request.OfficeId == null || distributionTransformer.OfficeId == request.OfficeId);
    }
}
