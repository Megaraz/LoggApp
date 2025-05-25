using System.Text;
using AppLogic.Models.Weather;

namespace AppLogic.Models.DTOs.Detailed
{
    public class AirQualityDataDetailed
    {
        /// <summary>
        /// Tidpunkten för mätningen.
        /// </summary>
        public int? Time { get; set; }

        public Measurement<double?>? UVI { get; set; }
        public Measurement<double?>? AQI { get; set; }
        public Measurement<double?>? PM25 { get; set; }
        public Measurement<double?>? Ozone { get; set; }
        public Measurement<double?>? CO { get; set; }
        public Measurement<double?>? NO2 { get; set; }
        public Measurement<double?>? Dust { get; set; }



        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Time < 10)
            {
                sb.Append('0');
            }

            sb.Append($"{Time}:00\t");

            if (UVI.Value.HasValue) sb.Append($"{UVI.Value:F1}\t");
            if (AQI.Value.HasValue) sb.Append($"{AQI.Value:F1}\t");
            if (PM25.Value.HasValue) sb.Append($"{PM25.Value:F1}\t");
            if (Ozone.Value.HasValue) sb.Append($"{Ozone.Value:F1}\t");
            if (CO.Value.HasValue) sb.Append($"{CO.Value:F1}\t");
            if (NO2.Value.HasValue) sb.Append($"{NO2.Value:F1}\t");
            if (Dust.Value.HasValue) sb.Append($"{Dust.Value:F1}\t");


            return sb.ToString();
        }
    }
}
