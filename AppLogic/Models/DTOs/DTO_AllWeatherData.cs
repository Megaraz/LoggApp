using System.Text;
using AppLogic.Models.Weather;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllWeatherData
    {
        public int WeatherDataId { get; set; }
        public int? DayCardId { get; set; }
        public List<HourlyWeatherData>? HourlyWeatherData { get; set; } = new List<HourlyWeatherData>();

        public string MainHeader { get; set; } = "TIME\tTEMP\tFEELSLIKE\tHUMIDITY\tPRECIP\tRAIN\tCLOUD\tUV\tWIND\tPRESSURE";



        public override string ToString()
        {
            var sb = new StringBuilder();
            //sb.Append($"WeatherDataId: {WeatherDataId}");

            //if (DayCardId.HasValue)
            //    sb.Append($", DayCardId: {DayCardId.Value}");


            sb.AppendLine(MainHeader);

            if (HourlyWeatherData != null && HourlyWeatherData.Any())
            {
                //sb.AppendLine();
                //sb.AppendLine("Hourly readings:");

                var first = HourlyWeatherData.FirstOrDefault();
                if (first is not null)
                {
                    sb.AppendLine(string.Join("\t", new[]
                    {
                        "", // För TIME
                        first.Temperature2m.Unit,
                        first.ApparentTemperature.Unit,
                        "",
                        first.RelativeHumidity2m.Unit,
                        "", // extra tabb efter Humidity
                        first.Precipitation.Unit,
                        first.Rain.Unit,
                        first.CloudCover.Unit,
                        first.UvIndex.Unit,
                        first.WindSpeed10m.Unit,
                        first.PressureMsl.Unit
                    }));
                }

                sb.AppendLine();


                foreach (var hour in HourlyWeatherData)
                {
                    sb.AppendLine($"{hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }

}
