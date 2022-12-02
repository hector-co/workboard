namespace Workboard.Application.Queries.Boards;

public record ColumnDto(
    int Id,
    string Name,
    CardState CardState,
    int Order);
