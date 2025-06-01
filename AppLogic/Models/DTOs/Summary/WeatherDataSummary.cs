using System.Text;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities.WeatherAndAQI;
using Microsoft.IdentityModel.Tokens;

namespace AppLogic.Models.DTOs.Summary
{
    public class WeatherDataSummary : IPromptRenderable
    {
        public int WeatherDataId { get; set; }
        public int? DayCardId { get; set; }
        public List<WeatherDataDetailed>? WeatherDataDetails { get; set; } = new List<WeatherDataDetailed>();

        public string? AISummary { get; set; }

        public WeatherDataSummary(WeatherData weatherData)
        {
            // Säkerställ att HourlyBlock finns
            var block = weatherData.HourlyBlock
                        ?? throw new ArgumentNullException(nameof(weatherData.HourlyBlock));
            var units = weatherData.HourlyUnits
                        ?? throw new ArgumentNullException(nameof(weatherData.HourlyBlock));

            AISummary = weatherData.AISummary;

            // Mappa varje tidpunkt + index till en HourlyWeatherData
            WeatherDataDetails = block.Time
                .Select((time, i) => new WeatherDataDetailed
                {
                    Time = time.Hour,
                    Temperature2m = new Measurement<double?>
                    {
                        Value = block.Temperature2m.ElementAtOrDefault(i),
                        Unit = units.Temperature2m
                    },
                    ApparentTemperature = new Measurement<double?>
                    {
                        Value = block.ApparentTemperature.ElementAtOrDefault(i),
                        Unit = units.ApparentTemperature
                    },
                    RelativeHumidity2m = new Measurement<double?>
                    {
                        Value = block.RelativeHumidity2m.ElementAtOrDefault(i),
                        Unit = units.RelativeHumidity2m
                    },
                    DewPoint2m = new Measurement<double?>()
                    {
                        Value = block.DewPoint2m.ElementAtOrDefault(i),
                        Unit = units.DewPoint2m
                    },
                    Precipitation = new Measurement<double?>
                    {
                        Value = block.Precipitation.ElementAtOrDefault(i),
                        Unit = units.Precipitation
                    },
                    Rain = new Measurement<double?>
                    {
                        Value = block.Rain.ElementAtOrDefault(i),
                        Unit = units.Rain
                    },
                    CloudCover = new Measurement<double?>
                    {
                        Value = block.CloudCover.ElementAtOrDefault(i),
                        Unit = units.CloudCover
                    },
                    UvIndex = new Measurement<double?>
                    {
                        Value = block.UvIndex.ElementAtOrDefault(i),
                        Unit = units.UvIndex
                    },
                    WindSpeed10m = new Measurement<double?>
                    {
                        Value = block.WindSpeed10m.ElementAtOrDefault(i),
                        Unit = units.WindSpeed10m
                    },
                    PressureMsl = new Measurement<double?>
                    {
                        Value = block.PressureMsl.ElementAtOrDefault(i),
                        Unit = units.PressureMsl
                    },
                    IsDay = new Measurement<double?>()
                    {
                        Value = block.IsDay.ElementAtOrDefault(i),


                    }
                })
                .ToList();
        }
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
