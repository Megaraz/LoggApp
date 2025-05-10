using System.Text.Json.Serialization;

namespace BusinessLogic.Models.Weather
{
    public class CloudCoverBlock
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";


        [JsonPropertyName("afternoon")]
        public int? Afternoon { get; set; }
    }

}