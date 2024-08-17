using OpsManagerAPI.Application.Features.Reconnections.Commands;

namespace OpsManagerAPI.Application.Features.Reconnections.Validators;

public class ApproveReconnectionTicketCommandRequestValidator : AbstractValidator<ApproveReconnectionTicketCommand>
{
    public ApproveReconnectionTicketCommandRequestValidator()
    {
        RuleFor(x => x.ReconnectionTicketId)
            .NotEmpty().WithMessage("Reconnection TicketID is required.");
    }
}
