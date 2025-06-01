using System.Text.Json.Serialization;

namespace AppLogic.Models
{
    /// <summary>
    /// Response model for geographical results from the open-meteo.com API.
    /// </summary>
    public class GeoResultResponse
    {
        [JsonPropertyName("results")]
        public List<GeoResult> Results { get; set; } = new();

        [JsonPropertyName("generationtime_ms")]
        public double? GenerationTimeMs { get; set; }
    }
}
