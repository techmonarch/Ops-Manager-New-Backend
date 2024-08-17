using OpsManagerAPI.Application.Features.Complaints.Commands;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

namespace OpsManagerAPI.Application.Features.Complaints.Validators;
public class CreateComplaintCommandRequestValidator : AbstractValidator<CreateComplaintCommand>
{
    public CreateComplaintCommandRequestValidator()
    {
        RuleFor(x => x.Comment)
              .NotEmpty().WithMessage("Comment is required.");

        RuleFor(x => x.SubCategoryId)
            .NotEmpty().WithMessage("SubCategory must be a present.");

        RuleFor(x => x.CategoryId)
              .NotEmpty().WithMessage("Category is required.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image is required");

        RuleFor(x => x)
                   .Must(HasValidCustomerOrTransformerId)
                   .WithMessage("Either CustomerId or DistributionTransformerId must be present, but not both.");
    }

    private bool HasValidCustomerOrTransformerId(CreateComplaintCommand command)
    {
        // Check if exactly one of CustomerId or DistributionTransformerId is present
        return command.CustomerId.HasValue ^ command.DistributionTransformerId.HasValue;
    }
}
