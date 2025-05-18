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
            return $"[{Date}] AQI: {AirQualitySummary?.MaxAqi}, Temp: {WeatherSummary?.MaxTemp}°C";

        }
    }

}
