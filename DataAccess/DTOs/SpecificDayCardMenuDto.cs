using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.DTOs
{
    public class SpecificDayCardMenuDto
    {
        public int DayCardId { get; set; }
        public int? UserId { get; set; }
        public DateOnly Date { get; set; }

        public AllAirQualitiesMenuDto? AirQualitySummary { get; set; }
        public AllWeatherDataMenuDto? WeatherSummary { get; set; }

        public override string ToString()
        {
            return $"[{Date}] AQI: {AirQualitySummary?.MaxAqi}, Temp: {WeatherSummary?.MaxTemp}°C";
        }
    }

}
