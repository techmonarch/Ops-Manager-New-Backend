using OpsManagerAPI.Application.Features.Reconnections.Commands;

namespace OpsManagerAPI.Application.Features.Reconnections.Validators;

public class CloseReconnectionTicketCommandRequestValidator : AbstractValidator<CloseReconnectionTicketCommand>
{
    public CloseReconnectionTicketCommandRequestValidator()
    {
        RuleFor(x => x.ReconnectionTicketId)
        .NotEmpty().WithMessage("Reconnection TicketID is required.");

        RuleFor(x => x.Image)
        .NotEmpty().WithMessage("Image is required.");

        RuleFor(x => x.Comment)
        .NotEmpty().WithMessage("Comment is required.");
    }
}
