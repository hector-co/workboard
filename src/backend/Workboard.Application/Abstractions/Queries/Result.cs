using System.Text.Json.Serialization;

namespace Workboard.Application.Abstractions.Queries;

public class Result<TData>
{
    public Result(TData? data, int? totalCount = default)
    {
        Data = data;
        TotalCount = totalCount;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData? Data { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Meta { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalCount { get; }

    public Result<TData> AddMeta(string key, object value)
    {
        Meta ??= new Dictionary<string, object>();

        Meta.Add(key, value);

        return this;
    }
}