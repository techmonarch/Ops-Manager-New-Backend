using OpsManagerAPI.Application.Features.Notifications.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsManagerAPI.Application.Features.Notifications.Validators;
public class NotificationCommandRequestValidator : AbstractValidator<CreateNotificationCommand>
{
    public NotificationCommandRequestValidator()
    {
        RuleFor(x => x.Title)
       .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.Topic)
       .NotEmpty().WithMessage("Topic is required.");

        RuleFor(x => x.Body)
       .NotEmpty().WithMessage("Body is required.");
    }
}
