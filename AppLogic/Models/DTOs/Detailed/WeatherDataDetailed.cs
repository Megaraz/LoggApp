using System.Text;

namespace AppLogic.Models.DTOs.Detailed
{
    /// <summary>
    /// DTO for detailed weather data, including various measurements such as temperature, humidity, precipitation, and more.
    /// </summary>
    public class WeatherDataDetailed
    {
        public int? Time { get; set; }

        public Measurement<double?>? Temperature2m { get; set; }
        public Measurement<double?>? ApparentTemperature { get; set; }
        public Measurement<double?>? RelativeHumidity2m { get; set; }
        public Measurement<double?>? DewPoint2m { get; set; }
        public Measurement<double?>? Precipitation { get; set; }
        public Measurement<double?>? Rain { get; set; }
        public Measurement<double?>? CloudCover { get; set; }
        public Measurement<double?>? UvIndex { get; set; }
        public Measurement<double?>? WindSpeed10m { get; set; }
        public Measurement<double?>? PressureMsl { get; set; }
        public Measurement<double?>? IsDay { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.Append($"0{Time}:00\t");

            if (Temperature2m.Value.HasValue) sb.Append($"{Temperature2m.Value:F1}\t");
            if (ApparentTemperature.Value.HasValue) sb.Append($"{ApparentTemperature.Value:F1}\t");
            if (RelativeHumidity2m.Value.HasValue) sb.Append($"\t{RelativeHumidity2m.Value:F1}\t\t");
            //if (DewPoint2m.Value.HasValue) sb.Append($", DewPt: {DewPoint2m.Value:F1}°C");
            if (Precipitation.Value.HasValue) sb.Append($"{Precipitation.Value:F1}\t");
            if (Rain.Value.HasValue) sb.Append($"{Rain.Value:F1}\t");
            if (CloudCover.Value.HasValue) sb.Append($"{CloudCover.Value:F1}\t");
            if (UvIndex.Value.HasValue) sb.Append($"{UvIndex.Value:F1}\t");
            if (WindSpeed10m.Value.HasValue) sb.Append($"{WindSpeed10m.Value:F1}\t");
            if (PressureMsl.Value.HasValue) sb.Append($"{PressureMsl.Value:F1}\t");
            //if (IsDay.Value.HasValue) sb.Append($", IsDay: {(IsDay.Value > 0.5 ? "Yes" : "No")}");

            return sb.ToString();
        }
    }
}
