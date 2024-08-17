using OpsManagerAPI.Application.Features.Teams.Commands;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;

namespace OpsManagerAPI.Application.Features.Teams.Validators;

public class AddMemberCommandRequestValidator : AbstractValidator<AddMemberCommand>
{
    private readonly IRepositoryWithEvents<Team> _teamRepository;
    public AddMemberCommandRequestValidator(IRepositoryWithEvents<Team> teamRepository)
    {
        _teamRepository = teamRepository;

        RuleFor(x => x.StaffMemberIds)
             .NotEmpty().WithMessage("At leaset a member is required");

        RuleFor(x => x.TeamId)
            .NotEmpty()
                .MustAsync(async (id, ct) => await _teamRepository.GetByIdAsync(id, ct) is not null)
                    .WithMessage((_, id) => $"Team with Id {id} not found");
    }
}