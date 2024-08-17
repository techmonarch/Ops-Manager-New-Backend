﻿namespace OpsManagerAPI.Application.Features.Customers.Dtos;

public record EvaluationsAndEnumerationsCustomerDataDto(
    string Id,
    string AccountNumber,
    string? MeterNumber,
    string Phone,
    string City,
    string State,
    string ServiceBand,
    string CustomerType,
    string AccountType,
    string Status,
    string FirstName,
    string LastName,
    string MiddleName,
    string Address,
    string Email,
    string DistributionTransformerId,
    bool IsCustomerEvaluated,
    bool IsCustomerEnumerated
) : IDto;
