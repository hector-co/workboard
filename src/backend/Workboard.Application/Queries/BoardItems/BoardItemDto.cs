using QueryX.Attributes;
using Workboard.Application.Queries.Boards;
using Workboard.Application.Queries.Cards;

namespace Workboard.Application.Queries.BoardItems;

public record BoardItemDto(
    int Id,
    [property: QueryIgnore]
    BoardDto Board,
    ColumnDto Column,
    CardDto Card,
    int Order);
