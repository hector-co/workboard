using Workboard.Application.Abstractions.Commands;
using Workboard.Domain.Abstractions;
using Workboard.Domain.Model;

namespace Workboard.Application.Commands.Boards;

public class AddBoardItemHandler : ICommandHandler<AddBoardItem>
{
    private readonly IBoardRepository _boardRepo;
    private readonly IBoardItemRepository _boardItemRepo;
    private readonly ICardRepository _cardRepo;
    private readonly IDeveloperRepository _developerRepo;

    public AddBoardItemHandler(IBoardRepository boardRepository, IBoardItemRepository boardItemRepository, 
        ICardRepository cardRepository, IDeveloperRepository developerRepository)
    {
        _boardRepo = boardRepository;
        _boardItemRepo = boardItemRepository;
        _cardRepo = cardRepository;
        _developerRepo = developerRepository;
    }

    public async Task<Response> Handle(AddBoardItem request, CancellationToken cancellationToken)
    {
        var board = await _boardRepo.GetById(request.BoardId, cancellationToken);
        if (board == null)
        {
            return Response.Failure("BoardItem.Register.BoardNotFound", "Board not found");
        }

        var cardOwners = await _developerRepo.GetByIds(request.OwnersId, cancellationToken);

        var card = new Card(request.Name, request.Description, request.Priority, request.EstimatedPoints, cardOwners);

        await _cardRepo.Save(card, cancellationToken);


        var maxOrder = await _boardItemRepo.GetMaxOrder(board.Id, null);

        var boardItem = board.AddBoardItem(null, card, maxOrder + 1);

        await _boardItemRepo.Save(boardItem, cancellationToken);

        return Response.Success();
    }
}
