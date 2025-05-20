using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation;

namespace AppLogic.Models.DTOs
{
    public class DTO_SpecificDayCard
    {
        public int DayCardId { get; set; }
        public int? UserId { get; set; }
        public DateOnly Date { get; set; }

        public DTO_AllAirQualities? AirQualitySummary { get; set; }
        public DTO_AllWeatherData? WeatherSummary { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Date: {Date:yyyy-MM-dd}");

            if (AirQualitySummary?.HourlyAirQualityData != null && AirQualitySummary.HourlyAirQualityData.Count > 0)
            {
                var avgBirch = AirQualitySummary.HourlyAirQualityData.Average(d => d.BirchPollen ?? 0);
                var avgAQI = AirQualitySummary.HourlyAirQualityData.Average(d => d.AQI ?? 0);
                sb.AppendLine($"  Avg Birch Pollen: {avgBirch:F1}");
                sb.AppendLine($"  Avg AQI: {avgAQI:F1}");
            }
            else
            {
                sb.AppendLine("  [No air quality data]");
            }

            if (WeatherSummary?.HourlyWeatherData != null && WeatherSummary.HourlyWeatherData.Any())
            {
                var morning = WeatherSummary.HourlyWeatherData
                    .Where(h => h.Time >= 6 && h.Time <= 9) // Exempelvis morgonperiod
                    .ToList();

                if (morning.Any())
                {
                    var avgTemp = morning.Average(h => h.Temperature2m ?? 0);
                    var avgFeels = morning.Average(h => h.ApparentTemperature ?? 0);
                    var maxWind = morning.Max(h => h.WindSpeed10m ?? 0);
                    sb.AppendLine($"  Morning Avg Temp: {avgTemp:F1}°C");
                    sb.AppendLine($"  Morning Feels Like: {avgFeels:F1}°C");
                    sb.AppendLine($"  Max Wind Speed (6–9): {maxWind:F1} m/s");
                }
                else
                {
                    sb.AppendLine("  [No morning weather data]");
                }
            }
            else
            {
                sb.AppendLine("  [No weather data]");
            }

            return sb.ToString().TrimEnd();
        }

    }
}


