using Workboard.Application.Abstractions.Queries;

namespace Workboard.Application.Queries.Cards;

public record GetCardDtoById(int Id) : IQuery<CardDto>;