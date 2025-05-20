using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Models.Weather.AirQuality
{
    public class HourlyPollenData
    {
        public int? Time { get; set; }
        public Measurement<double?>? BirchPollen { get; set; }
        public Measurement<double?>? AlderPollen { get; set; }
        public Measurement<double?>? GrassPollen { get; set; }
        public Measurement<double?>? MugwortPollen { get; set; }
        public Measurement<double?>? RagweedPollen { get; set; }



        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"0{Time}:00\n");
            sb.Append($"  [POLLEN]\n");
            if (BirchPollen.Value.HasValue) sb.Append($"\tBirch: {BirchPollen.Value:F1} {BirchPollen.Unit} | ");
            if (AlderPollen.Value.HasValue) sb.Append($"Alder: {AlderPollen.Value:F1} {AlderPollen.Unit} | ");
            if (GrassPollen.Value.HasValue) sb.Append($"Grass: {GrassPollen.Value:F1}  {GrassPollen.Unit} | ");
            if (MugwortPollen.Value.HasValue) sb.Append($"Mugwort: {MugwortPollen.Value:F1}  {MugwortPollen.Unit} | ");
            if (RagweedPollen.Value.HasValue) sb.Append($"Ragweed: {RagweedPollen.Value:F1} {RagweedPollen.Unit}\n");

            return sb.ToString().TrimEnd();
        }
    }
}
