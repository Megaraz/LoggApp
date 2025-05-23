using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Intake.Enums;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllCaffeineDrinks
    {

        public int? DayCardId { get; set; }

        public List<DTO_SpecificCaffeineDrink> HourlyCaffeineData { get; set; } = new List<DTO_SpecificCaffeineDrink>();

        public int? TotalCaffeineInMg { get; set; }



        public override string ToString()
        {
            var sb = new StringBuilder();

            var props = typeof(DTO_SpecificCaffeineDrink).GetProperties();
            string mainHeader = string.Empty;

            foreach (var prop in props)
            {
                mainHeader += prop.Name.ToUpper() + '\t';
            }

            sb.AppendLine(mainHeader);

            foreach (var drink in HourlyCaffeineData)
            {
                sb.AppendLine($"{drink}");
            }

            return sb.ToString();

        }
    }
}
