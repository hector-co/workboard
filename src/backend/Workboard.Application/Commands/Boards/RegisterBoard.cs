using Workboard.Application.Abstractions.Commands;

namespace Workboard.Application.Commands.Boards;

public record RegisterBoard(string Name) : ICommand<int>;