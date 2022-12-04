using FluentValidation;

namespace Workboard.Application.Commands.Boards;

public class AddBoardItemValidator : AbstractValidator<AddBoardItem>
{
    public AddBoardItemValidator()
    {
        RuleFor(c => c.Name)
            .MaximumLength(100);
        RuleFor(c => c.Priority)
            .InclusiveBetween(0, 2);
    }
}
