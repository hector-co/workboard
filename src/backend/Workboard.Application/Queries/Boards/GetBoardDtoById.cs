using Workboard.Application.Abstractions.Queries;

namespace Workboard.Application.Queries.Boards;

public record GetBoardDtoById(int Id) : IQuery<BoardDto>;