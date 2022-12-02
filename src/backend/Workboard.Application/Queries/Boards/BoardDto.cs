namespace Workboard.Application.Queries.Boards;

public record BoardDto(
    int Id,
    string Name,
    List<ColumnDto> Columns);
