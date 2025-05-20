using System.Text;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllAirQualities
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }

        public List<HourlyAirQualityData>? HourlyAirQualityData { get; set; } = new List<HourlyAirQualityData>();


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"AirQualityId: {AirQualityId}");
            if (DayCardId.HasValue) sb.Append($", DayCardId: {DayCardId.Value}");

            if (HourlyAirQualityData != null && HourlyAirQualityData.Any())
            {
                sb.AppendLine();
                sb.AppendLine("Hourly readings:");
                foreach (var hour in HourlyAirQualityData)
                {
                    sb.AppendLine($"{hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }

}
