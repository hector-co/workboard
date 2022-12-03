using Mapster;
using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Queries.Boards;
using QueryX;

namespace Workboard.Infrastructure.DataAccess.EF.Boards.Queries;

public class ListBoardDtoHandler : IQueryHandler<ListBoardDto, IEnumerable<BoardDto>>
{
    private readonly WorkboardContext _context;

    public ListBoardDtoHandler(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<BoardDto>>> Handle(ListBoardDto request, CancellationToken cancellationToken)
    {
        var queryable = _context.Set<Board>()
            .AsNoTracking();

        queryable = queryable.ApplyQuery(request, applyOrderingAndPaging: false);
        var totalCount = await queryable.CountAsync(cancellationToken);
        queryable = queryable.ApplyOrderingAndPaging(request);

        var data = await queryable.ToListAsync(cancellationToken);

        return new Result<IEnumerable<BoardDto>>(data.Adapt<List<BoardDto>>(), totalCount);
    }
}