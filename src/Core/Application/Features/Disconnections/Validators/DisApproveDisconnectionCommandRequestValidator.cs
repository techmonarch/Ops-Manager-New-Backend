using OpsManagerAPI.Application.Features.Disconnections.Commands;

namespace OpsManagerAPI.Application.Features.Disconnections.Validators;

public class DisApproveDisconnectionCommandRequestValidator : AbstractValidator<DisApproveDisconnectionTicketCommand>
{
    public DisApproveDisconnectionCommandRequestValidator()
    {
        RuleFor(x => x.DisconnectionTicketId)
           .NotEmpty().WithMessage("Disconnection Ticket ID is required");
    }
}

