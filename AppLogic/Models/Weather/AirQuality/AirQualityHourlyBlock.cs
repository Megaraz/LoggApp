using System.Text.Json.Serialization;

namespace AppLogic.Models.Weather.AirQuality
{
    public class AirQualityHourlyBlock
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("time")]
        public List<DateTime>? Time { get; set; }


        [JsonPropertyName("alder_pollen")]
        public List<double>? AlderPollen { get; set; }


        [JsonPropertyName("birch_pollen")]
        public List<double>? BirchPollen { get; set; }


        [JsonPropertyName("grass_pollen")]
        public List<double>? GrassPollen { get; set; }


        [JsonPropertyName("mugwort_pollen")]
        public List<double>? MugwortPollen { get; set; }


        [JsonPropertyName("ragweed_pollen")]
        public List<double>? RagweedPollen { get; set; }


        [JsonPropertyName("uv_index")]
        public List<double>? UVI{ get; set; }


        [JsonPropertyName("european_aqi")]
        public List<double>? AQI { get; set; }


        [JsonPropertyName("pm2_5")]
        public List<double>? PM25 { get; set; }


        [JsonPropertyName("ozone")]
        public List<double>? Ozone { get; set; }


        [JsonPropertyName("carbon_monoxide")]
        public List<double>? CarbonMonoxide { get; set; }


        [JsonPropertyName("nitrogen_dioxide")]
        public List<double>? NitrogenDioxide { get; set; }


        [JsonPropertyName("dust")]
        public List<double>? Dust { get; set; }





    }
}
