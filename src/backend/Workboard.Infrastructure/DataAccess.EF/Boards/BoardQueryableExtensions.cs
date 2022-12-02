using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Boards;

internal static class BoardQueryableExtensions
{
    internal static IQueryable<Board> AddIncludes
        (this IQueryable<Board> queryable)
    {
        return queryable
            .Include(m => m.Columns)
            ;
    }
}