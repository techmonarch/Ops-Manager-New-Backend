using OpsManagerAPI.Application.Features.Complaints.Commands;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;

namespace OpsManagerAPI.Application.Features.Complaints.Validators;

public class UpdateComplaintStatusCommandRequestValidator : AbstractValidator<UpdateComplaintStatusCommand>
{
    private readonly IRepositoryWithEvents<Complaint> _repository;
    public UpdateComplaintStatusCommandRequestValidator(IRepositoryWithEvents<Complaint> repository)
    {
        _repository = repository;
        RuleFor(x => x.ComplaintId)
            .NotEmpty()
                .MustAsync(async (id, ct) => await _repository.GetByIdAsync(id, ct) is not null)
                    .WithMessage((_, id) => $"Complaint with Id {id} not found");
        RuleFor(x => x.NewStatus)
           .NotEmpty().WithMessage("Complaint Status is required");
    }
}