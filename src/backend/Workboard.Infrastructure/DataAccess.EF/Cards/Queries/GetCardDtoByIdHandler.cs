using Mapster;
using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Queries.Cards;

namespace Workboard.Infrastructure.DataAccess.EF.Cards.Queries;

public class GetCardDtoByIdHandler : IQueryHandler<GetCardDtoById, CardDto>
{
    private readonly WorkboardContext _context;

    public GetCardDtoByIdHandler(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<Result<CardDto>> Handle(GetCardDtoById request, CancellationToken cancellationToken)
    {
        var data = await _context.Set<Card>()
            .AddIncludes()
            .AsNoTracking()
            .FirstOrDefaultAsync(m => request.Id == m.Id, cancellationToken);

        return new Result<CardDto>(data?.Adapt<CardDto>());
    }
}