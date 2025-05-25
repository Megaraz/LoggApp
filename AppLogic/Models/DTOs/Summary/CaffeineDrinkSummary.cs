using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Intake.Enums;

namespace AppLogic.Models.DTOs.Summary
{
    public class CaffeineDrinkSummary
    {

        public int? DayCardId { get; set; }

        public List<CaffeineDrinkDetailed> CaffeineDrinksDetails { get; set; } = new List<CaffeineDrinkDetailed>();

        public int? TotalCaffeineInMg { get; set; }



        public override string ToString()
        {
            var sb = new StringBuilder();

            var props = typeof(CaffeineDrinkDetailed).GetProperties();
            string mainHeader = string.Empty;

            foreach (var prop in props)
            {
                mainHeader += prop.Name.ToUpper() + '\t';
            }

            sb.AppendLine(mainHeader);

            foreach (var drink in CaffeineDrinksDetails)
            {
                sb.AppendLine($"{drink}");
            }

            return sb.ToString();

        }
    }
}
