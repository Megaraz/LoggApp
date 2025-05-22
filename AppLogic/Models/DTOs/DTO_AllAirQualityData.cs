using System.Text;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllAirQualityData
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }

        public List<HourlyAirQualityData>? HourlyAirQualityData { get; set; } = new List<HourlyAirQualityData>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            //sb.Append($"AirQualityId: {AirQualityId}");
            //if (DayCardId.HasValue) sb.Append($", DayCardId: {DayCardId.Value}");



            if (HourlyAirQualityData != null && HourlyAirQualityData.Any())
            {


                var props = typeof(HourlyAirQualityData).GetProperties();
                string mainHeader = string.Empty;
                foreach (var prop in props)
                {
                    mainHeader += prop.Name.ToUpper() + "\t";
                }

                sb.AppendLine(mainHeader);


                var first = HourlyAirQualityData.FirstOrDefault();
                if (first is not null)
                {
                    sb.AppendLine(string.Join("\t", new[]
                    {
                        "",
                        first.UVI.Unit,
                        first.AQI.Unit,
                        first.PM25.Unit,
                        first.Ozone.Unit,
                        first.CO.Unit,
                        first.NO2.Unit,
                        first.Dust.Unit
                    }));
                }

                sb.AppendLine();

                foreach (var hour in HourlyAirQualityData)
                {
                    sb.AppendLine($"{hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }

}
