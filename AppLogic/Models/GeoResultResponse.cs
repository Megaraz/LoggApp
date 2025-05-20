using System.Text.Json.Serialization;

namespace AppLogic.Models
{
    public class GeoResultResponse
    {
        [JsonPropertyName("results")]
        public List<GeoResult> Results { get; set; } = new();

        [JsonPropertyName("generationtime_ms")]
        public double? GenerationTimeMs { get; set; }
    }
}
