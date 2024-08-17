using OpsManagerAPI.Application.Features.Reconnections.Commands;

namespace OpsManagerAPI.Application.Features.Reconnections.Validators;

public class AssignReconnectionTaskCommandRequestValidator : AbstractValidator<AssignReconnectionTasksCommand>
{
    public AssignReconnectionTaskCommandRequestValidator()
    {
        RuleFor(x => x.ReconnectionTaskIds)
         .NotEmpty().WithMessage("Reconnection TaskID is required.");

        RuleFor(x => x.TeamId)
          .NotEmpty().WithMessage("Team ID is required.");
    }
}
