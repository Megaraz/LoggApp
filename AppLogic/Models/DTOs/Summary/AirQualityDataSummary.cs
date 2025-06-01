using System.Text;
using AppLogic.Migrations;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities.WeatherAndAQI;
using Microsoft.IdentityModel.Tokens;

namespace AppLogic.Models.DTOs.Summary
{
    public class AirQualityDataSummary : IPromptRenderable
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }

        public List<AirQualityDataDetailed>? AirQualityDetails { get; set; } = new List<AirQualityDataDetailed>();

        public string? AISummary { get; set; }


        public AirQualityDataSummary(AirQualityData airQuality)
        {
            var block = airQuality.HourlyBlock ?? throw new ArgumentNullException(nameof(airQuality));
            var units = airQuality.HourlyUnits ?? throw new ArgumentNullException(nameof(airQuality));


            AISummary = airQuality.AQI_AISummary;
            AirQualityDetails = block.Time
                .Select((time, i) => new AirQualityDataDetailed()
                {
                    Time = time.Hour,

                    UVI = new Measurement<double?>
                    {
                        Value = block.UVI.ElementAtOrDefault(i),
                        Unit = units.UVI
                    },
                    AQI = new Measurement<double?>
                    {
                        Value = block.AQI.ElementAtOrDefault(i),
                        Unit = units.AQI
                    },
                    PM25 = new Measurement<double?>
                    {
                        Value = block.PM25.ElementAtOrDefault(i),
                        Unit = units.PM25
                    },
                    Ozone = new Measurement<double?>
                    {
                        Value = block.Ozone.ElementAtOrDefault(i),
                        Unit = units.Ozone
                    },
                    CO = new Measurement<double?>
                    {
                        Value = block.CarbonMonoxide.ElementAtOrDefault(i),
                        Unit = units.CO
                    },
                    NO2 = new Measurement<double?>
                    {
                        Value = block.NitrogenDioxide.ElementAtOrDefault(i),
                        Unit = units.NO2
                    },
                    Dust = new Measurement<double?>
                    {
                        Value = block.Dust.ElementAtOrDefault(i),
                        Unit = units.Dust
                    }

                }).
                ToList();
        }

        public string ToPrompt()
        => ToString();

        public override string ToString()
        {
            var sb = new StringBuilder();
            //sb.Append($"AirQualityId: {AirQualityId}");
            //if (DayCardId.HasValue) sb.Append($", DayCardId: {DayCardId.Value}");

            if (!AISummary.IsNullOrEmpty())
            {
                sb.AppendLine();
                sb.AppendLine(AISummary + "\n\n");
            }

            if (AirQualityDetails != null && AirQualityDetails.Any())
            {


                

                sb.AppendLine(DTOHelper.GetPropNamesAsHeader<AirQualityDataDetailed>("\t"));


                var first = AirQualityDetails.FirstOrDefault();
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

                foreach (var hour in AirQualityDetails)
                {
                    sb.AppendLine($"{hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }

}
