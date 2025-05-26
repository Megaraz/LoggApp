using System.Text.Json.Serialization;
using AppLogic.Interfaces;
using AppLogic.Models.DTOs;

namespace AppLogic.Models.Weather
{
    public class WeatherData : ITimeOfEntry, IPromptRenderable
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard? DayCard { get; set; }
        public TimeOnly? TimeOf { get; set; }

        public string? AISummary { get; set; }

        [JsonPropertyName("latitude")]
        public double? Lat { get; set; }

        [JsonPropertyName("longitude")]
        public double? Lon { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double? GenerationTimeMs { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int? UtcOffsetSeconds { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        [JsonPropertyName("hourly")]
        public WeatherDataHourlyBlock? HourlyBlock { get; set; }

        [JsonPropertyName("hourly_units")]
        public WeatherDataHourlyUnits? HourlyUnits { get; set; }

        public string ToPrompt()
        => ToString();

    }

}
