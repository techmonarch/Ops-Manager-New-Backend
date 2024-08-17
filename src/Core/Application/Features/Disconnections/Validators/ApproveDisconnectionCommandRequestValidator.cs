using OpsManagerAPI.Application.Features.Disconnections.Commands;

namespace OpsManagerAPI.Application.Features.Disconnections.Validators;

public class ApproveDisconnectionCommandRequestValidator : AbstractValidator<ApproveDisconnectionTicketCommand>
{
    public ApproveDisconnectionCommandRequestValidator()
    {
        RuleFor(x => x.DisconnectionTicketId)
           .NotEmpty().WithMessage("Disconnection Ticket ID is required");
    }
}

