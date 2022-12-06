using Workboard.Application.Abstractions.Commands;
using Workboard.Domain.Abstractions;
using Workboard.Domain.Model;

namespace Workboard.Application.Commands.Boards;

public class MoveBoardItemHandler : ICommandHandler<MoveBoardItem>
{
    private readonly IBoardRepository _boardRepo;
    private readonly IBoardItemRepository _boardItemRepo;

    public MoveBoardItemHandler(IBoardRepository boardRepository, IBoardItemRepository boardItemRepository)
    {
        _boardRepo = boardRepository;
        _boardItemRepo = boardItemRepository;
    }

    public async Task<Response> Handle(MoveBoardItem request, CancellationToken cancellationToken)
    {
        var board = await _boardRepo.GetById(request.BoardId, cancellationToken);

        if (board == null)
            return Response.Failure("BoardITem.MoveBoardtItem.BoardNotFound", "Entity not found");

        var item = await _boardItemRepo.GetById(request.BoardItemId, cancellationToken);

        if (item == null || !item.IsInBoard(board))
            return Response.Failure("BoardITem.MoveBoardtItem.NotFound", "Entity not found");

        Column? column = null;
        if (request.ColumnId != null)
        {
            column = board.Columns.FirstOrDefault(c => c.Id == request.ColumnId);
            if (column == null)
                return Response.Failure("BoardITem.MoveBoardtItem.ColumnNotFound", "Entity not found");
        }

        var fromColumnId = item.Column?.Id;
        var oldOrder = item.Order;

        item.MoveTo(column, request.Order);

        await _boardItemRepo.Save(item, cancellationToken);

        await _boardItemRepo.AdjustOrder(request.BoardItemId, fromColumnId, oldOrder, request.ColumnId, request.Order, 
            cancellationToken);

        return Response.Success();
    }
}
