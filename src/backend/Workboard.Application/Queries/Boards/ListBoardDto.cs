using QueryX;
using Workboard.Application.Abstractions.Queries;

namespace Workboard.Application.Queries.Boards;

public class ListBoardDto : Query<BoardDto>, IQuery<IEnumerable<BoardDto>>
{
}
