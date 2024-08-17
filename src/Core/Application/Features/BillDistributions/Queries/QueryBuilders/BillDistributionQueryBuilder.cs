using OpsManagerAPI.Application.Features.BillDistributions.Dtos;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.BillDistributions.Queries.QueryBuilders;
public static class BillDistributionQueryBuilder
{
    public static Expression<Func<BillDistribution, bool>> BuildFilter(BillDistributionFilterRequest request)
    {
        return customer =>
            (request.CreatedBy == null || customer.CreatedBy == DefaultIdType.Parse(request.CreatedBy)) &&
            (request.ApprovalStatus == null || customer.IsDelivered == request.ApprovalStatus) &&
            (request.StartDate == null || customer.LastModifiedOn >= request.StartDate) &&
            (request.EndDate == null || customer.LastModifiedOn <= request.EndDate);
    }
}