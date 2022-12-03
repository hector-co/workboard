using Mapster;
using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Queries.Developers;
using QueryX;

namespace Workboard.Infrastructure.DataAccess.EF.Developers.Queries;

public class ListDeveloperDtoHandler : IQueryHandler<ListDeveloperDto, IEnumerable<DeveloperDto>>
{
    private readonly WorkboardContext _context;

    public ListDeveloperDtoHandler(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<DeveloperDto>>> Handle(ListDeveloperDto request, CancellationToken cancellationToken)
    {
        var queryable = _context.Set<Developer>()
            .AsNoTracking();

        queryable = queryable.ApplyQuery(request, applyOrderingAndPaging: false);
        var totalCount = await queryable.CountAsync(cancellationToken);
        queryable = queryable.ApplyOrderingAndPaging(request);

        var data = await queryable.ToListAsync(cancellationToken);

        return new Result<IEnumerable<DeveloperDto>>(data.Adapt<List<DeveloperDto>>(), totalCount);
    }
}