using Mapster;
using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Queries.Cards;
using QueryX;

namespace Workboard.Infrastructure.DataAccess.EF.Cards.Queries;

public class ListCardDtoHandler : IQueryHandler<ListCardDto, IEnumerable<CardDto>>
{
    private readonly WorkboardContext _context;

    public ListCardDtoHandler(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<CardDto>>> Handle(ListCardDto request, CancellationToken cancellationToken)
    {
        var queryable = _context.Set<Card>()
            .AddIncludes() 
            .AsNoTracking();

        queryable = queryable.ApplyQuery(request, applyOrderingAndPaging: false);
        var totalCount = await queryable.CountAsync(cancellationToken);
        queryable = queryable.ApplyOrderingAndPaging(request);

        var data = await queryable.ToListAsync(cancellationToken);

        return new Result<IEnumerable<CardDto>>(data.Adapt<List<CardDto>>(), totalCount);
    }
}