using Workboard.Application.Abstractions.Commands;
using Workboard.Domain.Abstractions;
using Workboard.Domain.Model;

namespace Workboard.Application.Commands.Boards;

public class RegisterBoardHandler : ICommandHandler<RegisterBoard, int>
{
    private readonly static List<(string Name, int CardState)> _defaultColumns = new()
    {
        ("Not started", 0),
        ("In progress", 1),
        ("Done", 2)
    };

    private readonly IBoardRepository _boardRepo;

    public RegisterBoardHandler(IBoardRepository boardRepository)
    {
        _boardRepo = boardRepository;
    }

    public async Task<Response<int>> Handle(RegisterBoard request, CancellationToken cancellationToken)
    {
        var board = new Board(request.Name);

        foreach (var column in _defaultColumns)
        {
            board.AddColumn(column.Name, column.CardState);
        }

        await _boardRepo.Save(board, cancellationToken);

        return Response.Success(board.Id);
    }
}
