using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.BoardItems;

public class BoardItemRepository : IBoardItemRepository
{
    private readonly WorkboardContext _context;

    public BoardItemRepository(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<int> GetMaxOrder(int boardId, int? columnId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<BoardItem>()
            .Where(bi => bi.BoardId == boardId && bi.ColumnId == columnId)
            .OrderBy(bi => bi.Order)
            .Select(bi => bi.Order)
            .LastOrDefaultAsync(cancellationToken);
    }

    public async Task Save(BoardItem boardItem, CancellationToken cancellationToken = default)
    {
        _context.Add(boardItem);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
