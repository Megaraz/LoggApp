using System.Text;
using AppLogic.Models.DTOs.Detailed;
using Microsoft.IdentityModel.Tokens;

namespace AppLogic.Models.DTOs.Summary
{
    public class AirQualityDataSummary : IPromptRenderable
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }

        public List<AirQualityDataDetailed>? AirQualityDetails { get; set; } = new List<AirQualityDataDetailed>();

        public string? AISummary { get; set; }

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
