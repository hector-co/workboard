using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Cards;

internal static class CardQueryableExtensions
{
    internal static IQueryable<Card> AddIncludes
        (this IQueryable<Card> queryable)
    {
        return queryable
            .Include(m => m.Owners)
            ;
    }
}