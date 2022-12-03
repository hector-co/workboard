namespace Workboard.Domain.Model;

public interface ICardRepository
{
    Task Save(Card card, CancellationToken cancellationToken = default);
}
