using OpsManagerAPI.Application.Features.MeterReadings.Commands;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.MeterReadings.Validators;

public class DisApproveMeterReadingRequestValidator : AbstractValidator<DisApproveMeterReadingCommand>
{
    private readonly IRepositoryWithEvents<MeterReading> _repository;

    public DisApproveMeterReadingRequestValidator(IRepositoryWithEvents<MeterReading> repository)
    {
        _repository = repository;
        RuleFor(p => p.MeterReadingId)
                    .NotEmpty()
                        .MustAsync(async (id, ct) => await _repository.GetByIdAsync(id, ct) is not null)
                            .WithMessage((_, id) => $"Meter Reading with Id {id} not found");
    }
}