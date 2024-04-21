using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace bomber;

[UsedImplicitly]
public class JsonBar
{
    [JsonPropertyName("open")] public double Open { get; set; }

    [JsonPropertyName("high")] public double High { get; set; }

    [JsonPropertyName("low")] public double Low { get; set; }

    [JsonPropertyName("close")] public double Close { get; set; }
}