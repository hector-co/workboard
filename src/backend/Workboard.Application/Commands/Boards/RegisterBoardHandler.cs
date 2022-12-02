using Workboard.Application.Abstractions.Commands;
using Workboard.Domain.Abstractions;
using Workboard.Domain.Model;

namespace Workboard.Application.Commands.Boards;

public class RegisterBoardHandler : ICommandHandler<RegisterBoard, int>
{
    private readonly IBoardRepository _boardRepo;

    public RegisterBoardHandler(IBoardRepository boardRepository)
    {
        _boardRepo = boardRepository;
    }

    public async Task<Response<int>> Handle(RegisterBoard request, CancellationToken cancellationToken)
    {
        var board = new Board(request.Name);

        foreach (var column in request.Columns)
        {
            board.AddColumn(column.Name, column.CardState);
        }

        await _boardRepo.Save(board, cancellationToken);

        return Response.Success(board.Id);
    }
}
