using OpsManagerAPI.Application.Features.Enumerations.Commands;

namespace OpsManagerAPI.Application.Features.Enumerations.Validators;

public class CreateEnumerationCommandRequestValidator : AbstractValidator<CreateEnumerationCommand>
{
    public CreateEnumerationCommandRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.LGA)
            .NotEmpty().WithMessage("LGA is required.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.BuildingDescription)
            .NotEmpty().WithMessage("Building description is required.");

        RuleFor(x => x.Landmark)
            .NotEmpty().WithMessage("Landmark is required.");

        RuleFor(x => x.BusinessType)
            .NotEmpty().WithMessage("Business type is required.");

        RuleFor(x => x.PremiseType)
            .NotEmpty().WithMessage("Premise type is required.");

        RuleFor(x => x.ServiceBand)
            .NotEmpty().WithMessage("Service band is required.");

        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("Longitude is required.");

        RuleFor(x => x.Latitude)
            .NotEmpty().WithMessage("Latitude is required.");

        RuleFor(x => x.CustomerType)
            .IsInEnum().WithMessage("Customer type is required and must be a valid enum value.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Customer status is required and must be a valid enum value.");

        RuleFor(x => x.AccountType)
            .IsInEnum().WithMessage("Account type is required and must be a valid enum value.");

        RuleFor(x => x.IsMetered)
            .NotNull().WithMessage("IsMetered flag is required.");

        //RuleFor(x => x.MeterNumber)
        //    .NotEmpty().When(x => x.IsMetered).WithMessage("Meter number is required for metered customers.");

        RuleFor(x => x.TariffId)
            .NotNull().WithMessage("Tariff ID is required.");
    }
}
