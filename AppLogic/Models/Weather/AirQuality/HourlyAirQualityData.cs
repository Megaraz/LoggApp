using System.Text;

namespace AppLogic.Models.Weather.AirQuality
{
    public class HourlyAirQualityData
    {
        /// <summary>
        /// Tidpunkten för mätningen.
        /// </summary>
        public int? Time { get; set; }

        public Measurement<double?>? BirchPollen { get; set; }
        public Measurement<double?>? AlderPollen { get; set; }
        public Measurement<double?>? GrassPollen { get; set; }
        public Measurement<double?>? MugwortPollen { get; set; }
        public Measurement<double?>? RagweedPollen { get; set; }
        public Measurement<double?>? UVI { get; set; }
        public Measurement<double?>? AQI { get; set; }
        public Measurement<double?>? PM25 { get; set; }
        public Measurement<double?>? Ozone { get; set; }
        public Measurement<double?>? CarbonMonoxide { get; set; }
        public Measurement<double?>? NitrogenDioxide { get; set; }
        public Measurement<double?>? Dust { get; set; }



        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"[{Time}:00]\n");
            sb.Append($"  [POLLEN]\n");
            if (BirchPollen.Value.HasValue) sb.Append($"\tBirch: {BirchPollen.Value:F1} {BirchPollen.Unit} | ");
            if (AlderPollen.Value.HasValue) sb.Append($"Alder: {AlderPollen.Value:F1} {AlderPollen.Unit} | ");
            if (GrassPollen.Value.HasValue) sb.Append($"Grass: {GrassPollen.Value:F1}  {GrassPollen.Unit} | ");
            if (MugwortPollen.Value.HasValue) sb.Append($"Mugwort: {MugwortPollen.Value:F1}  {MugwortPollen.Unit} | ");
            if (RagweedPollen.Value.HasValue) sb.Append($"Ragweed: {RagweedPollen.Value:F1} {RagweedPollen.Unit}\n");
            sb.Append($"  [AIRQUALITY]\n");
            if (UVI.Value.HasValue) sb.Append($"\tUVI: {UVI.Value:F1} {UVI.Unit} | ");
            if (AQI.Value.HasValue) sb.Append($"AQI: {AQI.Value:F1} {AQI.Unit} | ");
            if (PM25.Value.HasValue) sb.Append($"PM2.5: {PM25.Value:F1} {PM25.Unit} | ");
            if (Ozone.Value.HasValue) sb.Append($"O₃: {Ozone.Value:F1} {Ozone.Unit} | ");
            if (CarbonMonoxide.Value.HasValue) sb.Append($"CO: {CarbonMonoxide.Value:F1} {CarbonMonoxide.Unit} | ");
            if (NitrogenDioxide.Value.HasValue) sb.Append($"NO₂: {NitrogenDioxide.Value:F1} {NitrogenDioxide.Unit} | ");
            if (Dust.Value.HasValue) sb.Append($"Dust: {Dust.Value:F1} {Dust.Unit}\n");

            return sb.ToString();
        }
    }
}
