using Mapster;
using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Queries.BoardItems;
using QueryX;

namespace Workboard.Infrastructure.DataAccess.EF.BoardItems.Queries;

public class ListBoardItemDtoHandler : IQueryHandler<ListBoardItemDto, IEnumerable<BoardItemDto>>
{
    private readonly WorkboardContext _context;

    public ListBoardItemDtoHandler(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<BoardItemDto>>> Handle(ListBoardItemDto request, CancellationToken cancellationToken)
    {
        var queryable = _context.Set<BoardItem>()
            .AddIncludes()
            .AsNoTracking();

        queryable = queryable.Where(b => b.BoardId == request.BoardId);

        queryable = queryable.ApplyQuery(request, applyOrderingAndPaging: false);
        var totalCount = await queryable.CountAsync(cancellationToken);
        queryable = queryable.ApplyOrderingAndPaging(request);

        var data = await queryable
            .OrderBy(b => b.ColumnId)
            .ThenBy(b => b.Order)
            .ToListAsync(cancellationToken);

        return new Result<IEnumerable<BoardItemDto>>(data.Adapt<List<BoardItemDto>>(), totalCount);
    }
}