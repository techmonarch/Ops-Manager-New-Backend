using OpsManagerAPI.Application.Features.Billings.Commands;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

namespace OpsManagerAPI.Application.Features.Billings.Validators;
public class BillingCommandRequestValidator : AbstractValidator<BillingCommand>
{
    private readonly IRepositoryWithEvents<Customer> _repository;
    public BillingCommandRequestValidator(IRepositoryWithEvents<Customer> repository)
    {
        _repository = repository;

        RuleFor(x => x.CustomerId)
                    .NotEmpty()
                        .MustAsync(async (id, ct) => await _repository.GetByIdAsync(id, ct) is not null)
                            .WithMessage((_, id) => $"Customer with Id {id} not found");
        RuleFor(x => x.BillDate)
           .Must(BeADateTime).WithMessage("BillDate must be present.");
        RuleFor(x => x.DueDate)
           .Must(BeADateTime).WithMessage("DueDate must be present.");
        RuleFor(x => x.Consumption)
           .Must(BeANumber).WithMessage("Consumption must be a number.");
        RuleFor(x => x.Arrears)
           .Must(BeANumber).WithMessage("Arrears must be present.");
        RuleFor(x => x.VAT)
           .Must(BeANumber).WithMessage("VAT must be present.");
        RuleFor(x => x.CurrentCharge)
           .Must(BeANumber).WithMessage("CurrentCharge must be present.");
        RuleFor(x => x.TotalCharge)
           .Must(BeANumber).WithMessage("TotalCharge must be present.");
        RuleFor(x => x.TotalDue)
           .Must(BeANumber).WithMessage("TotalDue must be present.");
        RuleFor(x => x.AccountType)
           .NotEmpty().WithMessage("AccountType must be present.");
        RuleFor(x => x.CustomerType)
           .NotEmpty().WithMessage("CustomerType must be present.");

    }

    private bool BeANumber(decimal value) => decimal.TryParse(value.ToString(), out _);
    private bool BeADateTime(DateTime value) => DateTime.TryParse(value.ToString(), out _);
}
