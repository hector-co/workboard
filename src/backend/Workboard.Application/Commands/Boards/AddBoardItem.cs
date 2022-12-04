using System.Text.Json.Serialization;
using Workboard.Application.Abstractions.Commands;

namespace Workboard.Application.Commands.Boards;

public record AddBoardItem(
    string Name,
    string Description,
    List<int> OwnersId,
    int Priority,
    decimal EstimatedPoints) : ICommand
{
    [JsonIgnore]
    public int BoardId { get; set; }
}
