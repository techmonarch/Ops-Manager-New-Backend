using OpsManagerAPI.Application.Features.Teams.Commands;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Teams.Validators;

public class ChangeStatusCommandRequestValidator : AbstractValidator<ChangeStatusCommand>
{
    private readonly IRepositoryWithEvents<Team> _repository;
    public ChangeStatusCommandRequestValidator(IRepositoryWithEvents<Team> repository)
    {
        _repository = repository;

        RuleFor(x => x.TeamId)
            .NotEmpty()
                .MustAsync(async (id, ct) => await _repository.GetByIdAsync(id, ct) is not null)
                    .WithMessage((_, id) => $"Team with Id {id} not found");
    }
}