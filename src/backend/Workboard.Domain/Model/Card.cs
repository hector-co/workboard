namespace Workboard.Domain.Model;

public partial class Card
{
#nullable disable
    private Card()
    {
        _owners = new();
    }
#nullable enable

    private readonly List<Developer> _owners;

    public Card(string name, string description, int priority, decimal estimatedPoints, List<Developer> owners)
    {
        Name = name;
        Description = description;
        _owners = owners;
        Priority = priority;
        EstimatedPoints = estimatedPoints;
        State = CardState.NotStarted;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public IEnumerable<Developer> Owners => _owners.AsReadOnly();
    public int Priority { get; private set; }
    public decimal EstimatedPoints { get; private set; }
    public int State { get; set; }
}
