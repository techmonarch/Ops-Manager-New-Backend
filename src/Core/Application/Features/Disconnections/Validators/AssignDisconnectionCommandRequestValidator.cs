using OpsManagerAPI.Application.Features.Reconnections.Commands;

namespace OpsManagerAPI.Application.Features.Disconnections.Validators;

public class AssignDisconnectionCommandRequestValidator : AbstractValidator<AssignDisconnectionTasksCommand>
{
    public AssignDisconnectionCommandRequestValidator()
    {
        RuleFor(x => x.TeamId)
           .NotEmpty().WithMessage("Team ID is required");

        RuleFor(x => x.DisconnectionTaskIds)
           .NotEmpty().WithMessage("Disconnection Task ID is required");
    }
}

