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

        if (request.ColumnId != null)
        {
            if (!board.Columns.Any(c => c.Id == request.ColumnId.Value))
            {
                return Response.Failure("BoardItem.Register.ColumnNotFound", "Column not found");
            }
        }

        var column = request.ColumnId == null
            ? null
            : board.Columns.First(c => c.Id == request.ColumnId);


        var cardOwners = await _developerRepo.GetByIds(request.OwnersId, cancellationToken);

        var card = new Card(request.Name, request.Description, request.Priority, request.EstimatedPoints, cardOwners);

        await _cardRepo.Save(card, cancellationToken);


        var maxOrder = await _boardItemRepo.GetMaxOrder(board.Id, column?.Id);

        var boardItem = board.AddBoardItem(column, card, maxOrder + 1);

        await _boardItemRepo.Save(boardItem, cancellationToken);

        return Response.Success();
    }
}
