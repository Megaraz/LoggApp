using System.Text.Json.Serialization;

namespace AppLogic.Models.Entities.WeatherAndAQI
{
    /// <summary>
    /// Represents a block of hourly weather data, including various meteorological parameters.
    /// JSON properties are mapped to the Open-Meteo API response structure.
    /// </summary>
    public class WeatherDataHourlyBlock
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("time")]
        public List<DateTime>? Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public List<double>? Temperature2m { get; set; }

        [JsonPropertyName("apparent_temperature")]
        public List<double>? ApparentTemperature { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public List<double>? RelativeHumidity2m { get; set; }

        [JsonPropertyName("dew_point_2m")]
        public List<double>? DewPoint2m { get; set; }

        [JsonPropertyName("precipitation")]
        public List<double>? Precipitation { get; set; }

        [JsonPropertyName("rain")]
        public List<double>? Rain { get; set; }

        [JsonPropertyName("cloud_cover")]
        public List<double>? CloudCover { get; set; }

        [JsonPropertyName("uv_index")]
        public List<double>? UvIndex { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public List<double>? WindSpeed10m { get; set; }

        [JsonPropertyName("pressure_msl")]
        public List<double>? PressureMsl { get; set; }

        [JsonPropertyName("is_day")]
        public List<double>? IsDay { get; set; }
    }
}
