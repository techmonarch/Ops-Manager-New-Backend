using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Billings.Queries.QueryBuilders;
public static class BillingQueryBuilder
{
    public static Expression<Func<Billing, bool>> BuildFilter(GetBillingQueries request)
    {
        return bill =>
            (request.CustomerId == null || bill.CustomerId == DefaultIdType.Parse(request.CustomerId)) &&
            (request.StartDate == null || bill.LastModifiedOn >= request.StartDate) &&
            (request.EndDate == null || bill.LastModifiedOn <= request.EndDate);
    }
}
