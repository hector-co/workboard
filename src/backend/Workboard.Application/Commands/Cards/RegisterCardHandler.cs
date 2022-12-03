using Workboard.Application.Abstractions.Commands;
using Workboard.Domain.Abstractions;
using Workboard.Domain.Model;

namespace Workboard.Application.Commands.Cards;

public class RegisterCardHandler : ICommandHandler<RegisterCard, int>
{
    private readonly ICardRepository _cardRepo;
    private readonly IDeveloperRepository _developerRepo;

    public RegisterCardHandler(ICardRepository cardRepository, IDeveloperRepository developerRepository)
    {
        _cardRepo = cardRepository;
        _developerRepo = developerRepository;
    }

    public async Task<Response<int>> Handle(RegisterCard request, CancellationToken cancellationToken)
    {
        var owners = await _developerRepo.GetByIds(request.OwnersId, cancellationToken);

        var card = new Card(request.Name, request.Description, request.Priority, request.EstimatedPoints, owners);

        await _cardRepo.Save(card, cancellationToken);

        return Response.Success(card.Id);
    }
}
