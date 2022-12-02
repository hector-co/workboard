using Workboard.Application.Abstractions.Commands;

namespace Workboard.Application.Commands.Boards;

public record RegisterBoard
(
    string Name,
    List<RegisterBoard.RegisterColumn> Columns
) : ICommand<int>
{
    public record RegisterColumn(
        string Name,
        int CardState);
}
