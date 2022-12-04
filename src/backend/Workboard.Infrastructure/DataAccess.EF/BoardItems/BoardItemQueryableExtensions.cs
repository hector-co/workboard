using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.BoardItems;

internal static class BoardItemQueryableExtensions
{
    internal static IQueryable<BoardItem> AddIncludes
        (this IQueryable<BoardItem> queryable)
    {
        return queryable
            .Include(m => m.Board)
            .Include(m => m.Column)
            .Include(m => m.Card)
            ;
    }
}