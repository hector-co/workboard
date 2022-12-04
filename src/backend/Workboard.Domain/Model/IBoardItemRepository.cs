namespace Workboard.Domain.Model;

public interface IBoardItemRepository
{
    Task<int> GetMaxOrder(int boardId, int? columnId, CancellationToken cancellationToken = default);
    Task Save(BoardItem boardItem, CancellationToken cancellationToken = default);
}
