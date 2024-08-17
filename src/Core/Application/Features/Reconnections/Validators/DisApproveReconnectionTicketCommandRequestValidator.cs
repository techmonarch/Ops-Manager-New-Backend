using OpsManagerAPI.Application.Features.Reconnections.Commands;

namespace OpsManagerAPI.Application.Features.Reconnections.Validators;

public class DisApproveReconnectionTicketCommandRequestValidator : AbstractValidator<DisApproveReconnectionTicketCommand>
{
    public DisApproveReconnectionTicketCommandRequestValidator()
    {
        RuleFor(x => x.ReconnectionTicketId)
         .NotEmpty().WithMessage("Reconnection TicketID is required.");
    }
}
