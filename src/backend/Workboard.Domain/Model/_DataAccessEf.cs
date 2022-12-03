using System;
using System.Collections.Generic;
using System.Text.Json;

#nullable disable

namespace Workboard.Domain.Model;

public partial class Developer
{
    internal List<Card> CardOwners { get; set; }
    internal List<CardTask> CardTaskOwners { get; set; }
}

public partial class CardTask
{
    internal int CardId { get; set; }
}

public partial class Column
{
    internal int BoardId { get; set; }
}

