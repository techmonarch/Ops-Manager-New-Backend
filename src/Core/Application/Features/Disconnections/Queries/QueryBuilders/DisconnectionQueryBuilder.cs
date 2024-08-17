using OpsManagerAPI.Application.Features.Disconnections.Dtos;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Disconnections.Queries.QueryBuilders;

public class DisconnectionQueryBuilder
{
    public static Expression<Func<Disconnection, bool>> BuildFilter(DisconnectionFilterRequest request)
    {
        return Disconnection =>
            (request.Name == null ||
            (Disconnection.Customer.FirstName == null && Disconnection.Customer.FirstName.Contains(request.Name)) ||
            (Disconnection.Customer.MiddleName == null && Disconnection.Customer.MiddleName.Contains(request.Name)) ||
            (Disconnection.Customer.LastName == null && Disconnection.Customer.LastName.Contains(request.Name))) &&
            (request.DisconnectionStatus == default || Disconnection.Status == request.DisconnectionStatus) &&
            (string.IsNullOrEmpty(request.Region) || Disconnection.Customer.State == request.Region) &&
            (string.IsNullOrEmpty(request.Hub) || Disconnection.Customer.City == request.Hub) &&
            (string.IsNullOrEmpty(request.ServiceCenter) || Disconnection.Customer.LGA == request.ServiceCenter) &&
            (string.IsNullOrEmpty(request.PhoneNumber) || Disconnection.Customer.Phone == request.PhoneNumber) &&
            (string.IsNullOrEmpty(request.AccountNumber) || Disconnection.Customer.AccountNumber == request.AccountNumber) &&
            (string.IsNullOrEmpty(request.MeterNumber) || Disconnection.Customer.MeterNumber == request.MeterNumber) &&
            (string.IsNullOrEmpty(request.Email) || Disconnection.Customer.Email == request.Email) &&
            (!request.Status.HasValue || Disconnection.Customer.Status == request.Status) &&
            (string.IsNullOrEmpty(request.CreatedBy) || Disconnection.CreatedBy == Guid.Parse(request.CreatedBy)) &&
            (!request.AccountType.HasValue || Disconnection.Customer.AccountType == request.AccountType) &&
            (!request.CustomerType.HasValue || Disconnection.Customer.CustomerType == request.CustomerType) &&
            (string.IsNullOrEmpty(request.ServiceBand) || Disconnection.Customer.Tariff.ServiceBandName == request.ServiceBand) &&
            (!request.DistributionTransformerId.HasValue || Disconnection.Customer.DistributionTransformer.Id == request.DistributionTransformerId);
    }
}
