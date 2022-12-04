using QueryX;
using Workboard.Application.Abstractions.Queries;

namespace Workboard.Application.Queries.BoardItems;

public class ListBoardItemDto : Query<BoardItemDto>, IQuery<IEnumerable<BoardItemDto>>
{
    public int BoardId { get; set; }
}
