namespace Workboard.Domain.Model
{
    public interface IBoardRepository
    {
        Task Save(Board board, CancellationToken cancellationToken = default);
    }
}
