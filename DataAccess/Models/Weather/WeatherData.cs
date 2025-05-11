using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Models.Weather
{
    public class WeatherData : ITimeOfEntry
    {
        public int Id { get; set; }
        public int? DayCardId { get; set; }
        public virtual DayCard? DayCard { get; set; }
        public TimeOnly? TimeOf { get; set; }

        [JsonPropertyName("units")]
        public string? Units { get; set; }

        [JsonPropertyName("lat")]
        public double? Lat { get; set; }

        [JsonPropertyName("lon")]
        public double? Lon { get; set; }

        [JsonPropertyName("temperature")]
        public TemperatureBlock? Temperature { get; set; }

        [JsonPropertyName("pressure")]
        public PressureBlock? Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public HumidityBlock? Humidity { get; set; }

        [JsonPropertyName("precipitation")]
        public PrecipitationBlock? Precipitation { get; set; }

        [JsonPropertyName("cloud_cover")]
        public CloudCoverBlock? CloudCover { get; set; }


        public override string ToString()
        {
            return $"Date {DayCard.Date.ToString()}"
                + $"{Temperature}";
        }
    }

}
