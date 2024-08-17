using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Domain.Enums;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Complaints.Queries.QueryBuilder;
public static class ComplaintQueryBuilder
{
    public static Expression<Func<Complaint, bool>> BuildFilter(GetComplaintsQuery request)
    {
        return complaint =>
            (request.Id == null || complaint.Id == request.Id) &&
            (request.ComplaintType == null ||
            (request.ComplaintType == ComplaintType.Customer && complaint.CustomerId != null) ||
            (request.ComplaintType == ComplaintType.DistributionTransformer && complaint.DistributionTransformerId != null));
    }
}
