using FluentValidation;

namespace Workboard.Application.Commands.Boards;

public class RegisterBoardValidator : AbstractValidator<RegisterBoard>
{
    public RegisterBoardValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(100);
    }
}