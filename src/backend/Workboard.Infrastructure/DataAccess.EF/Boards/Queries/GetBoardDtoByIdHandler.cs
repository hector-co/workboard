using Mapster;
using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Queries.Boards;

namespace Workboard.Infrastructure.DataAccess.EF.Boards.Queries;

public class GetBoardDtoByIdHandler : IQueryHandler<GetBoardDtoById, BoardDto>
{
    private readonly WorkboardContext _context;

    public GetBoardDtoByIdHandler(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<Result<BoardDto>> Handle(GetBoardDtoById request, CancellationToken cancellationToken)
    {
        var data = await _context.Set<Board>()
            .AddIncludes()
            .AsNoTracking()
            .FirstOrDefaultAsync(m => request.Id == m.Id, cancellationToken);

        return new Result<BoardDto>(data?.Adapt<BoardDto>());
    }
}