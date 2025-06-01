using System.Text.Json.Serialization;

namespace AppLogic.Models.Entities.WeatherAndAQI
{
    /// <summary>
    /// Represents the units of various weather data fields for hourly weather reports.
    /// JSON properties are mapped to the Open-Meteo API response structure.
    /// </summary>
    public class WeatherDataHourlyUnits
    {
        [JsonIgnore]
        public string Marker { get; set; } = "Exists";

        [JsonPropertyName("temperature_2m")]
        public string? Temperature2m { get; set; }

        [JsonPropertyName("apparent_temperature")]
        public string? ApparentTemperature { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public string? RelativeHumidity2m { get; set; }

        [JsonPropertyName("dew_point_2m")]
        public string? DewPoint2m { get; set; }

        [JsonPropertyName("precipitation")]
        public string? Precipitation { get; set; }

        [JsonPropertyName("rain")]
        public string? Rain { get; set; }

        [JsonPropertyName("cloud_cover")]
        public string? CloudCover { get; set; }

        [JsonPropertyName("uv_index")]
        public string? UvIndex { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public string? WindSpeed10m { get; set; }

        [JsonPropertyName("pressure_msl")]
        public string? PressureMsl { get; set; }

        [JsonPropertyName("is_day")]
        public string? IsDay { get; set; }
    }

}
