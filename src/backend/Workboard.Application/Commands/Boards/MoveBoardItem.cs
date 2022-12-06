using System.Text.Json.Serialization;
using Workboard.Application.Abstractions.Commands;

namespace Workboard.Application.Commands.Boards;

public record MoveBoardItem(
    int? ColumnId,
    int Order) : ICommand
{
    [JsonIgnore]
    public int BoardId { get; set; }
    [JsonIgnore]
    public int BoardItemId { get; set; }
}
