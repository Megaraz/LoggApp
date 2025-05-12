using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace AppLogic.Models.Weather.AirQuality
{
    public class AirQuality : ITimeOfEntry
    {
        public int Id { get; set; }
        public int? DayCardId { get; set; }
        public virtual DayCard? DayCard { get; set; }
        public TimeOnly? TimeOf { get; set; }


        [JsonPropertyName("latitude")]
        public double? Lat { get; set; }

        [JsonPropertyName("longitude")]
        public double? Lon { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationTime_ms { get; set; }

        [JsonPropertyName("hourly")]
        public AirQualityHourlyBlock? HourlyBlock { get; set; }


    }
}
