using Workboard.Domain.Model;

namespace Workboard.Infrastructure.DataAccess.EF.Cards;

public class CardRepository : ICardRepository
{
    private readonly WorkboardContext _context;

    public CardRepository(WorkboardContext context)
    {
        _context = context;
    }

    public async Task Save(Card card, CancellationToken cancellationToken = default)
    {
        if (card.Id == 0)
            _context.Add(card);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
