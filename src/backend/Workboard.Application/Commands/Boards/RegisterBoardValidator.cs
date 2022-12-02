using FluentValidation;

namespace Workboard.Application.Commands.Boards;

public class RegisterBoardValidator : AbstractValidator<RegisterBoard>
{
    public RegisterBoardValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(100);
        RuleForEach(c => c.Columns)
            .SetValidator(new RegisterColumnValidator());
    }

    public class RegisterColumnValidator : AbstractValidator<RegisterBoard.RegisterColumn>
    {
        public RegisterColumnValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(100);
            RuleFor(c => c.CardState)
                .InclusiveBetween(0, 4);
        }
    }

}