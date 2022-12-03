using Workboard.Application.Queries.Developers;

namespace Workboard.Application.Queries.Cards;

public record CardDto(
    int Id,
    string Name,
    string Description,
    List<DeveloperDto> Owners,
    CardPriority Priority,
    decimal EstimatedPoints,
    CardState State);
