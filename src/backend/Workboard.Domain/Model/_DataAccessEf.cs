using System;
using System.Collections.Generic;
using System.Text.Json;

#nullable disable

namespace Workboard.Domain.Model;

public partial class Developer
{
    internal List<Card> CardOwners { get; set; }
}

public partial class Column
{
    internal int BoardId { get; set; }
}

public partial class BoardItem
{
    internal int BoardId { get; set; }
    internal int CardId { get; set; }
    internal int? ColumnId { get; set; }
}

