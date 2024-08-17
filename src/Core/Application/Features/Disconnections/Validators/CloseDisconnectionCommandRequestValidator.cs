using OpsManagerAPI.Application.Features.Disconnections.Commands;

namespace OpsManagerAPI.Application.Features.Disconnections.Validators;

public class CloseDisconnectionCommandRequestValidator : AbstractValidator<CloseDisconnectionTicketCommand>
{
    public CloseDisconnectionCommandRequestValidator()
    {
        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image is required");

        RuleFor(x => x.Comment)
           .NotEmpty().WithMessage("Reason is required");

        RuleFor(x => x.DisconnectionTicketId)
           .NotEmpty().WithMessage("DisConnection Ticket ID is required");
    }
}