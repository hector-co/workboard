namespace Workboard.Domain.Model;

public interface IDeveloperRepository
{
    Task<List<Developer>> GetByIds(List<int> ids, CancellationToken cancellationToken = default);
}
