using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllAirQualities
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }
        public double? MaxAqi { get; set; }
        public double? MaxBirchPollen { get; set; }

        public override string ToString()
        {
            return $"[AQI ID: {AirQualityId}] Max AQI: {MaxAqi}, Max Birch Pollen: {MaxBirchPollen}";
        }
    }

}
