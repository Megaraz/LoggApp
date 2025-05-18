using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Weather;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllWeatherData
    {
        public int WeatherDataId { get; set; }
        public int? DayCardId { get; set; }
        public List<HourlyWeatherData>? HourlyWeatherData { get; set; } = new List<HourlyWeatherData>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"WeatherDataId: {WeatherDataId}");

            if (DayCardId.HasValue)
                sb.Append($", DayCardId: {DayCardId.Value}");

            if (HourlyWeatherData != null && HourlyWeatherData.Any())
            {
                sb.AppendLine();
                sb.AppendLine("Hourly readings:");
                foreach (var hour in HourlyWeatherData)
                {
                    sb.AppendLine($"  - {hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }

}
