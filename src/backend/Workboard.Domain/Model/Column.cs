namespace Workboard.Domain.Model;

public partial class Column
{
#nullable disable 
    private Column() { }
#nullable enable

    internal Column(string name, int cardState, int order)
    {
        Name = name;
        CardState = cardState;
        Order = order;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int CardState { get; private set; }
    public int Order { get; private set; }
}
