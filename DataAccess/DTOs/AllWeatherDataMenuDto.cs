using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.DTOs
{
    public class AllWeatherDataMenuDto
    {
        public int WeatherDataId { get; set; }
        public int? DayCardId { get; set; }
        public double? MaxTemp { get; set; }
        public double? MaxPrecipitation { get; set; }

        public override string ToString()
        {
            return $"[Weather ID: {WeatherDataId}] Temp: {MaxTemp}°C, Rain: {MaxPrecipitation} mm";
        }
    }

}
