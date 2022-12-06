namespace Workboard.Domain.Model;

public partial class BoardItem
{
#nullable disable
    private BoardItem() { }
#nullable enable

    internal BoardItem(Board board, Column? column, Card card, int order)
    {
        Board = board;
        Column = column;
        Card = card;
        Order = order;
    }

    public int Id { get; private set; }
    public Board Board { get; private set; }
    public Column? Column { get; private set; }
    public Card Card { get; private set; }
    public int Order { get; private set; }

    public bool IsInBoard(Board board)
    {
        return BoardId == board.Id;
    }

    public void MoveTo(Column? column, int order)
    {
        Column = column;
        Order = order;
        if (Column == null)
        {
            Card.State = CardState.NotStarted;
        }
        else
        {
            Card.State = Column.CardState;
        }
    }
}
