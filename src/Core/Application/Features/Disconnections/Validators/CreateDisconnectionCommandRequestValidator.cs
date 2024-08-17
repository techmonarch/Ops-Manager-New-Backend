﻿using OpsManagerAPI.Application.Features.Disconnections.Commands;

namespace OpsManagerAPI.Application.Features.Disconnections.Validators;
public class CreateDisconnectionCommandRequestValidator : AbstractValidator<CreateDisconnectionTicketCommand>
{
    public CreateDisconnectionCommandRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Reason is required.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image  is required.");

        RuleFor(x => x.Latitude)
           .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }
}