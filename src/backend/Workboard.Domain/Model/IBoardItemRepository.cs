namespace Workboard.Domain.Model;

public interface IBoardItemRepository
{
    Task<BoardItem?> GetById(int id, CancellationToken cancellationToken = default);
    Task<int> GetMaxOrder(int boardId, int? columnId, CancellationToken cancellationToken = default);
    Task AdjustOrder(int id, int? fromColumnId, int oldOrder, int? toColumnId, int newOrder, CancellationToken cancellationToken = default);
    Task Save(BoardItem boardItem, CancellationToken cancellationToken = default);
}
