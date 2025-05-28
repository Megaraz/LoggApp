using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using Microsoft.IdentityModel.Tokens;

namespace AppLogic.Models.DTOs.Summary
{
    public class PollenDataSummary : IPromptRenderable
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }

        public List<PollenDataDetailed>? PollenDataDetails { get; set; } = new List<PollenDataDetailed>();
        public string? AISummary { get; set; }

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
