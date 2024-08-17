using OpsManagerAPI.Application.Features.BillDistributions.Commands;

namespace OpsManagerAPI.Application.Features.BillDistributions.Validators;

public class DistributeBillCommandRequestValidator : AbstractValidator<DistributeBillCommand>
{
    public DistributeBillCommandRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId must be present.");

        RuleFor(x => x.BillAmount)
            .Must(BeANumber).WithMessage("Bill amount must be a number.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }

    private bool BeANumber(decimal value) => decimal.TryParse(value.ToString(), out _);
}
