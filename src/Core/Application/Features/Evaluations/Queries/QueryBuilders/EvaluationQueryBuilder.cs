using OpsManagerAPI.Application.Features.Evaluations.Dtos;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Evaluations.Queries.QueryBuilders;
public static class EvaluationQueryBuilder
{
    public static Expression<Func<Evaluation, bool>> BuildFilter(EvaluationFilterRequest request)
    {
        return customer =>
            (request.CreatedBy == null || customer.CreatedBy == DefaultIdType.Parse(request.CreatedBy)) &&
            (request.StartDate == null || customer.LastModifiedOn >= request.StartDate) &&
            (request.EndDate == null || customer.LastModifiedOn <= request.EndDate);
    }
}