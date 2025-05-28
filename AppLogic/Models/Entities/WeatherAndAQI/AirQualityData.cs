using System.Text.Json.Serialization;
using AppLogic.Interfaces;
using AppLogic.Models.DTOs;

namespace AppLogic.Models.Entities.WeatherAndAQI
{
    public class AirQualityData
    {
        public int Id { get; set; }
        public int? DayCardId { get; set; }
        public virtual DayCard? DayCard { get; set; }

        public string? AQI_AISummary { get; set; }
        public string? Pollen_AISummary { get; set; }

        // These members are fetched and mapped from Open-Meteo API
        [JsonPropertyName("latitude")]
        public double? Lat { get; set; }

        [JsonPropertyName("longitude")]
        public double? Lon { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationTime_ms { get; set; }

        [JsonPropertyName("hourly_units")]
        public AirQualityHourlyUnits? HourlyUnits { get; set; }

        [JsonPropertyName("hourly")]
        public AirQualityHourlyBlock? HourlyBlock { get; set; }


    }
}
