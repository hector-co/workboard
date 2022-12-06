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

    public async Task<BoardItem?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<BoardItem>()
            .Include(i => i.Card).Include(i => i.Column)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<int> GetMaxOrder(int boardId, int? columnId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<BoardItem>()
            .Where(bi => bi.BoardId == boardId && bi.ColumnId == columnId)
            .OrderBy(bi => bi.Order)
            .Select(bi => bi.Order)
            .LastOrDefaultAsync(cancellationToken);
    }

    public async Task AdjustOrder(int id, int? fromColumnId, int oldOrder, int? toColumnId, int newOrder, CancellationToken cancellationToken = default)
    {
        await _context.Database.ExecuteSqlRawAsync($@"
            UPDATE {WorkboardContext.DbSchema}.""BoardItem"" 
            SET ""Order""=""Order""-1
            WHERE ""Id""<>{{0}} AND ""ColumnId""={{1}} AND ""Order"">{{2}}",
            new object[] { id, fromColumnId, oldOrder }, cancellationToken);

        await _context.Database.ExecuteSqlRawAsync($@"
            UPDATE {WorkboardContext.DbSchema}.""BoardItem"" 
            SET ""Order""=""Order""+1
            WHERE ""Id""<>{{0}} AND ""ColumnId""={{1}} AND ""Order"">={{2}}",
            new object[] { id, toColumnId, newOrder }, cancellationToken);
    }

    public async Task Save(BoardItem boardItem, CancellationToken cancellationToken = default)
    {
        if (boardItem.Id == 0)
            _context.Add(boardItem);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
