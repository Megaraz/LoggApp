using System.Text;

namespace AppLogic.Models.Weather
{
    public class HourlyWeatherData
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
            sb.Append($"Time: {Time}");

            if (Temperature2m.Value.HasValue) sb.Append($", Temp: {Temperature2m.Value:F1}°C");
            if (ApparentTemperature.Value.HasValue) sb.Append($", FeelsLike: {ApparentTemperature.Value:F1}°C");
            if (RelativeHumidity2m.Value.HasValue) sb.Append($", RH: {RelativeHumidity2m.Value:F1}%");
            if (DewPoint2m.Value.HasValue) sb.Append($", DewPt: {DewPoint2m.Value:F1}°C");
            if (Precipitation.Value.HasValue) sb.Append($", Precip: {Precipitation.Value:F1} mm");
            if (Rain.Value.HasValue) sb.Append($", Rain: {Rain.Value:F1} mm");
            if (CloudCover.Value.HasValue) sb.Append($", Cloud: {CloudCover.Value:F1}%");
            if (UvIndex.Value.HasValue) sb.Append($", UV: {UvIndex.Value:F1}");
            if (WindSpeed10m.Value.HasValue) sb.Append($", Wind: {WindSpeed10m.Value:F1} m/s");
            if (PressureMsl.Value.HasValue) sb.Append($", Pressure: {PressureMsl.Value:F1} hPa");
            if (IsDay.Value.HasValue) sb.Append($", IsDay: {(IsDay.Value > 0.5 ? "Yes" : "No")}");

            return sb.ToString();
        }
    }
}
