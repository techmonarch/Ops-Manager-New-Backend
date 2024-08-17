using OpsManagerAPI.Application.Features.Payments.Dtos;
using OpsManagerAPI.Domain.Aggregates.PaymentAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Payments.Queries.QueryBuilders;
public static class PaymentQueryBuilder
{
    public static Expression<Func<Payment, bool>> BuildFilter(GetPaymentQuery request)
    {
        return payment =>
            (request.CustomerId == null || payment.CustomerId == DefaultIdType.Parse(request.CustomerId)) &&
            (request.StartDate == null || payment.LastModifiedOn >= request.StartDate) &&
            (request.EndDate == null || payment.LastModifiedOn <= request.EndDate);
    }
}
