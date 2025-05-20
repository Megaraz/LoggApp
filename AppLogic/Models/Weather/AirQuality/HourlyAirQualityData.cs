using System.Text;

namespace AppLogic.Models.Weather.AirQuality
{
    public class HourlyAirQualityData
    {
        /// <summary>
        /// Tidpunkten för mätningen.
        /// </summary>
        public int? Time { get; set; }

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
            sb.Append($"0{Time}:00\n");

            if (UVI.Value.HasValue) sb.Append($"{UVI.Value:F1}\t");
            if (AQI.Value.HasValue) sb.Append($"{AQI.Value:F1}\t");
            if (PM25.Value.HasValue) sb.Append($"{PM25.Value:F1}\t");
            if (Ozone.Value.HasValue) sb.Append($"{Ozone.Value:F1}\t");
            if (CarbonMonoxide.Value.HasValue) sb.Append($"{CarbonMonoxide.Value:F1}\t");
            if (NitrogenDioxide.Value.HasValue) sb.Append($"{NitrogenDioxide.Value:F1}\t");
            if (Dust.Value.HasValue) sb.Append($"{Dust.Value:F1}\t");


            return sb.ToString();
        }
    }
}
