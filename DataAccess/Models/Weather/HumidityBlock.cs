using System.Text.Json.Serialization;

namespace BusinessLogic.Models.Weather
{
    public class HumidityBlock
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("afternoon")]
        public double? Afternoon { get; set; }
    }

}