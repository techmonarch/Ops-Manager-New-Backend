using OpsManagerAPI.Application.Features.Reconnections.Dtos;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using System.Linq.Expressions;
namespace OpsManagerAPI.Application.Features.Reconnections.Queries.QueryBuilders;
public class ReconnectionQueryBuilder
{
    public static Expression<Func<Reconnection, bool>> BuildFilter(ReconnectionFilterRequest request)
    {
        return reconnection =>
                (request.Name == null ||
                (reconnection.Customer.FirstName == null && reconnection.Customer.FirstName.Contains(request.Name)) ||
                (reconnection.Customer.MiddleName == null && reconnection.Customer.MiddleName.Contains(request.Name)) ||
                (reconnection.Customer.LastName == null && reconnection.Customer.LastName.Contains(request.Name))) &&
                (request.ReconnectionStatus == default || reconnection.Status == request.ReconnectionStatus) &&
                (string.IsNullOrEmpty(request.Region) || reconnection.Customer.State == request.Region) &&
                (string.IsNullOrEmpty(request.Hub) || reconnection.Customer.City == request.Hub) &&
                (string.IsNullOrEmpty(request.ServiceCenter) || reconnection.Customer.LGA == request.ServiceCenter) &&
                (string.IsNullOrEmpty(request.PhoneNumber) || reconnection.Customer.Phone == request.PhoneNumber) &&
                (string.IsNullOrEmpty(request.AccountNumber) || reconnection.Customer.AccountNumber == request.AccountNumber) &&
                (string.IsNullOrEmpty(request.MeterNumber) || reconnection.Customer.MeterNumber == request.MeterNumber) &&
                (string.IsNullOrEmpty(request.Email) || reconnection.Customer.Email == request.Email) &&
                (!request.Status.HasValue || reconnection.Customer.Status == request.Status) &&
                (string.IsNullOrEmpty(request.CreatedBy) || reconnection.CreatedBy == Guid.Parse(request.CreatedBy)) &&
                (!request.AccountType.HasValue || reconnection.Customer.AccountType == request.AccountType) &&
                (!request.CustomerType.HasValue || reconnection.Customer.CustomerType == request.CustomerType) &&
                (string.IsNullOrEmpty(request.ServiceBand) || reconnection.Customer.Tariff.ServiceBandName == request.ServiceBand) &&
                (!request.DistributionTransformerId.HasValue || reconnection.Customer.DistributionTransformer.Id == request.DistributionTransformerId);
    }
}
