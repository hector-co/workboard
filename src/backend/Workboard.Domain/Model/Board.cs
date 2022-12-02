namespace Workboard.Domain.Model;

public partial class Board
{
#nullable disable
    private Board() { }
#nullable enable

    private readonly List<Column> _columns;

    public Board(string name)
    {
        _columns = new List<Column>();

        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<Column> Columns => _columns.AsReadOnly();

    public Column AddColumn(string name, int cardState)
    {
        var nextOrder = _columns.Count == 0 ? 1 : _columns.Max(c => c.Order) + 1;
        var column = new Column(name, cardState, nextOrder);

        _columns.Add(column);
        return column;
    }
}