using System.Text.Json.Serialization;

namespace BusinessLogic.Models.Weather
{
    public class PrecipitationBlock
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("total")]
        public double? Total { get; set; }
    }

}