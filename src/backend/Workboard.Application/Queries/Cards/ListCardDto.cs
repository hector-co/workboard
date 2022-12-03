using QueryX;
using Workboard.Application.Abstractions.Queries;

namespace Workboard.Application.Queries.Cards;

public class ListCardDto : Query<CardDto>, IQuery<IEnumerable<CardDto>>
{
}
