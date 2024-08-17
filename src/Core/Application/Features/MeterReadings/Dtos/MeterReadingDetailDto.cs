using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.MeterReadings.Dtos;

public record MeterReadingDetailDto(
    string FirstName,
    string LastName,
    string AccountNumber,
    string MeterNumber,
    string Tariff,
    decimal PreviousReading,
    decimal PresentReading,
    decimal Consumption,
    string DSSName,
    string MeterReadingType,
    bool IsApproved,
    string? ImagePath
);
