using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Weather;

namespace AppLogic.Models.DTOs.Detailed
{
    public class PollenDataDetailed
    {
        public int? Time { get; set; }
        public Measurement<double?>? Birch { get; set; }
        public Measurement<double?>? Alder { get; set; }
        public Measurement<double?>? Grass { get; set; }
        public Measurement<double?>? Mugwort { get; set; }
        public Measurement<double?>? Ragweed { get; set; }



        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Time < 10)
            {
                sb.Append('0');
            }

            sb.Append($"{Time}:00\t");

            if (Birch.Value.HasValue) sb.Append($"{Birch.Value:F1}\t");
            if (Alder.Value.HasValue) sb.Append($"{Alder.Value:F1}\t");
            if (Grass.Value.HasValue) sb.Append($"{Grass.Value:F1}\t");
            if (Mugwort.Value.HasValue) sb.Append($"{Mugwort.Value:F1}\t");
            if (Ragweed.Value.HasValue) sb.Append($"{Ragweed.Value:F1}\n");

            return sb.ToString().TrimEnd();
        }
    }
}
