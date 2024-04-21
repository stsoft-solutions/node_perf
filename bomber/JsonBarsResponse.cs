using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace bomber;

[UsedImplicitly]
public class JsonBarsResponse
{
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; } = string.Empty;
    [JsonPropertyName("bars")] public JsonBar[] Bars { get; set; } = [];
}