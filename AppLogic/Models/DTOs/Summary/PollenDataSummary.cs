using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Migrations;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities.WeatherAndAQI;
using Microsoft.IdentityModel.Tokens;

namespace AppLogic.Models.DTOs.Summary
{
    public class PollenDataSummary : IPromptRenderable
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }

        public List<PollenDataDetailed>? PollenDataDetails { get; set; } = new List<PollenDataDetailed>();
        public string? AISummary { get; set; }

        public PollenDataSummary(AirQualityData airQuality)
        {
            var block = airQuality.HourlyBlock ?? throw new ArgumentNullException(nameof(airQuality));
            var units = airQuality.HourlyUnits ?? throw new ArgumentNullException(nameof(airQuality));

            AISummary = airQuality.Pollen_AISummary;
            PollenDataDetails = block.Time
            .Select((time, i) => new PollenDataDetailed()
            {
                Time = time.Hour,

                Birch = new Measurement<double?>
                {
                    Value = block.BirchPollen.ElementAtOrDefault(i),
                    Unit = units.BirchPollen
                },
                Alder = new Measurement<double?>
                {
                    Value = block.AlderPollen.ElementAtOrDefault(i),
                    Unit = units.AlderPollen
                },
                Grass = new Measurement<double?>
                {
                    Value = block.GrassPollen.ElementAtOrDefault(i),
                    Unit = units.GrassPollen
                },
                Mugwort = new Measurement<double?>
                {
                    Value = block.MugwortPollen.ElementAtOrDefault(i),
                    Unit = units.MugwortPollen
                },
                Ragweed = new Measurement<double?>
                {
                    Value = block.RagweedPollen.ElementAtOrDefault(i),
                    Unit = units.RagweedPollen
                }
            }).ToList();
        }

        public string ToPrompt()
        => ToString();

        public override string ToString()
        {
            var sb = new StringBuilder();


            if (!AISummary.IsNullOrEmpty())
            {
                sb.AppendLine();
                sb.AppendLine(AISummary + "\n\n");
            }

            if (PollenDataDetails != null && PollenDataDetails.Any())
            {

                var props = typeof(PollenDataDetailed).GetProperties();
                string mainHeader = "Units: grains/m3\n".ToUpperInvariant();




                foreach (var prop in props)
                {
                    mainHeader += prop.Name.ToUpper() + "\t";
                }

                sb.AppendLine(mainHeader + '\n');
                

                foreach (var hour in PollenDataDetails)
                {
                    sb.AppendLine($"{hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
