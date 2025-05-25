using System.Text;
using AppLogic.Models.DTOs.Detailed;
using Microsoft.IdentityModel.Tokens;

namespace AppLogic.Models.DTOs.Summary
{
    public class WeatherDataSummary : IPromptRenderable
    {
        public int WeatherDataId { get; set; }
        public int? DayCardId { get; set; }
        public List<WeatherDataDetailed>? WeatherDataDetails { get; set; } = new List<WeatherDataDetailed>();

        public string? AISummary { get; set; }
        

        public string ToPrompt()
        => ToString();

        public override string ToString()
        {
            var sb = new StringBuilder();
            //sb.Append($"WeatherDataId: {WeatherDataId}");

            //if (DayCardId.HasValue)
            //    sb.Append($", DayCardId: {DayCardId.Value}");

            if (!AISummary.IsNullOrEmpty())
            {
                sb.AppendLine();
                sb.AppendLine(AISummary + "\n\n");
            }
            string mainHeader = "TIME\tTEMP\tFEELSLIKE\tHUMIDITY\tPRECIP\tRAIN\tCLOUD\tUV\tWIND\tPRESSURE";
            sb.AppendLine(mainHeader);

            if (WeatherDataDetails != null && WeatherDataDetails.Any())
            {
                //sb.AppendLine();
                //sb.AppendLine("Hourly readings:");

                var first = WeatherDataDetails.FirstOrDefault();
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


                foreach (var hour in WeatherDataDetails)
                {
                    sb.AppendLine($"{hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }

}
