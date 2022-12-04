namespace Workboard.Domain.Model
{
    public interface IBoardRepository
    {
        Task<Board?> GetById(int id, CancellationToken cancellationToken = default);
        Task Save(Board board, CancellationToken cancellationToken = default);
    }
}
