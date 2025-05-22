using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;

namespace AppLogic.Models.DTOs
{
    public class DTO_AllPollenData
    {
        public int AirQualityId { get; set; }
        public int? DayCardId { get; set; }

        public List<HourlyPollenData>? HourlyPollenData { get; set; } = new List<HourlyPollenData>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (HourlyPollenData != null && HourlyPollenData.Any())
            {

                var props = typeof(HourlyPollenData).GetProperties();
                string mainHeader = "Units: grains/m3\n".ToUpperInvariant();




                foreach (var prop in props)
                {
                    mainHeader += prop.Name.ToUpper() + "\t";
                }

                sb.AppendLine(mainHeader + '\n');
                

                foreach (var hour in HourlyPollenData)
                {
                    sb.AppendLine($"{hour}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
