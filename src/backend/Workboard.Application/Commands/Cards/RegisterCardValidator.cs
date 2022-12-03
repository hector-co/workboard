using FluentValidation;

namespace Workboard.Application.Commands.Cards;

public class RegisterCardValidator : AbstractValidator<RegisterCard>
{
    public RegisterCardValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(100);
        RuleFor(c => c.Priority)
            .InclusiveBetween(0, 2);
    }

}