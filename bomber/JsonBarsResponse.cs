using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace bomber;

[UsedImplicitly]
public class JsonBarsResponse
{
    [JsonPropertyName("bars")] public JsonBar[] Bars { get; set; } = Array.Empty<JsonBar>();
}