using OpsManagerAPI.Application.Features.Reconnections.Commands;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.Reconnections.Validators;
public class CreateReconnectionCommandRequestValidator : AbstractValidator<CreateReconnectionTicketCommand>
{
    public CreateReconnectionCommandRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Reason is required");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image is required.");
    }
}
