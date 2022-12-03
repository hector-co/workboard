using Workboard.Application.Abstractions.Commands;

namespace Workboard.Application.Commands.Cards;

public record RegisterCard
(
    string Name,
    string Description,
    List<int> OwnersId,
    int Priority,
    decimal EstimatedPoints
) : ICommand<int>;
