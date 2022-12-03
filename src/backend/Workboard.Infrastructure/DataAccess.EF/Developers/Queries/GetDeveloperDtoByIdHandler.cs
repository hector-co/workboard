using Mapster;
using Microsoft.EntityFrameworkCore;
using Workboard.Domain.Model;
using Workboard.Application.Abstractions.Queries;
using Workboard.Application.Queries.Developers;

namespace Workboard.Infrastructure.DataAccess.EF.Developers.Queries;

public class GetDeveloperDtoByIdHandler : IQueryHandler<GetDeveloperDtoById, DeveloperDto>
{
    private readonly WorkboardContext _context;

    public GetDeveloperDtoByIdHandler(WorkboardContext context)
    {
        _context = context;
    }

    public async Task<Result<DeveloperDto>> Handle(GetDeveloperDtoById request, CancellationToken cancellationToken)
    {
        var data = await _context.Set<Developer>()
            .AsNoTracking()
            .FirstOrDefaultAsync(m => request.Id == m.Id, cancellationToken);

        return new Result<DeveloperDto>(data?.Adapt<DeveloperDto>());
    }
}