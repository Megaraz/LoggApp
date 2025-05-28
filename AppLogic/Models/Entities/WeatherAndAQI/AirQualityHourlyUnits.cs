using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppLogic.Models.Entities.WeatherAndAQI
{
    public class AirQualityHourlyUnits
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("alder_pollen")]
        public string? AlderPollen { get; set; }

        [JsonPropertyName("birch_pollen")]
        public string? BirchPollen { get; set; }

        [JsonPropertyName("grass_pollen")]
        public string? GrassPollen { get; set; }

        [JsonPropertyName("mugwort_pollen")]
        public string? MugwortPollen { get; set; }

        [JsonPropertyName("ragweed_pollen")]
        public string? RagweedPollen { get; set; }

        [JsonPropertyName("uv_index")]
        public string? UVI { get; set; }

        [JsonPropertyName("european_aqi")]
        public string? AQI { get; set; }

        [JsonPropertyName("pm2_5")]
        public string? PM25 { get; set; }

        [JsonPropertyName("ozone")]
        public string? Ozone { get; set; }

        [JsonPropertyName("carbon_monoxide")]
        public string? CO { get; set; }

        [JsonPropertyName("nitrogen_dioxide")]
        public string? NO2 { get; set; }

        [JsonPropertyName("dust")]
        public string? Dust { get; set; }
    }
}
