using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Boards
{
    public class BoardRepository : IBoardRepository
    {
        private readonly WorkboardContext _context;

        public BoardRepository(WorkboardContext context)
        {
            _context = context;
        }

        public async Task Save(Board board, CancellationToken cancellationToken = default)
        {
            _context.Add(board);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
