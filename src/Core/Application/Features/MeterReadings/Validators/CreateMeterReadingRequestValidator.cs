using OpsManagerAPI.Application.Features.MeterReadings.Commands;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.MeterReadings.Validators;

public class CreateMeterReadingRequestValidator : AbstractValidator<CreateMeterReadingCommand>
{
    public CreateMeterReadingRequestValidator()
    {
        RuleFor(x => x.PresentReading)
            .GreaterThan(0).WithMessage("Present reading must be greater than zero.");

        RuleFor(x => x)
            .Must(x => ValidateMeterReadingType(x))
            .WithMessage("Either CustomerId or DistributionTransformerId must be present based on MeterReadingType.");

        RuleFor(x => x.Latitude)
           .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }

    private static bool ValidateMeterReadingType(CreateMeterReadingCommand command) =>
        (command.MeterReadingType == MeterReadingType.Customer && command.CustomerId.HasValue) ||
        (command.MeterReadingType == MeterReadingType.DistributionTransformer && command.DistributionTransformerId.HasValue);
}
