using OpsManagerAPI.Application.Features.Customers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsManagerAPI.Application.Features.Customers.Validators;
 public class CreateCustomerCommandRequestValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandRequestValidator()
    {
        RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account number is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("Middle name is required.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is not a valid email address.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.LGA)
            .NotEmpty().WithMessage("LGA is required.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("Longitude is required.");

        RuleFor(x => x.Latitude)
            .NotEmpty().WithMessage("Latitude is required.");

        RuleFor(x => x.CustomerType)
            .IsInEnum().WithMessage("Customer type is required and must be a valid enum value.");

        RuleFor(x => x.AccountType)
            .IsInEnum().WithMessage("Account type is required and must be a valid enum value.");

        RuleFor(x => x.MeterNumber)
            .NotEmpty().WithMessage("Meter number is required for metered customers.");

        RuleFor(x => x.TariffId)
            .NotNull().WithMessage("Tariff ID is required.");

        RuleFor(x => x.DistributionTransformerId)
            .NotNull().WithMessage("DistributionTransformer ID is required.");

    }

}
