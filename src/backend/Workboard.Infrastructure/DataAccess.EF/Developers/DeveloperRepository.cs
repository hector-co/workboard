using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Developers;

public class DeveloperRepository : IDeveloperRepository
{
    private readonly WorkboardContext _context;

    public DeveloperRepository(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<List<Developer>> GetByIds(List<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Developer>()
            .Where(d => ids.Contains(d.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
