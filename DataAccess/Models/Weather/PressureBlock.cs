using System.Text.Json.Serialization;

namespace BusinessLogic.Models.Weather
{
    public class PressureBlock
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("afternoon")]
        public double? Afternoon { get; set; }
    }

}