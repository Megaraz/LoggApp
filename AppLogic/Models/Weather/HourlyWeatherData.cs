using System.Text;

namespace AppLogic.Models.Weather
{
    public class HourlyWeatherData
    {
        /// <summary>
        /// Tidpunkten för mätningen (UNIX-tid eller timme).
        /// </summary>
        public int? Time { get; set; }

        /// <summary>
        /// Lufttemperatur vid 2 m höjd (°C).
        /// </summary>
        public double? Temperature2m { get; set; }

        /// <summary>
        /// Känns-som-temperatur (°C).
        /// </summary>
        public double? ApparentTemperature { get; set; }

        /// <summary>
        /// Relativ fuktighet vid 2 m höjd (%).
        /// </summary>
        public double? RelativeHumidity2m { get; set; }

        /// <summary>
        /// Daggpunkts­temperatur vid 2 m höjd (°C).
        /// </summary>
        public double? DewPoint2m { get; set; }

        /// <summary>
        /// Nederbörd (mm).
        /// </summary>
        public double? Precipitation { get; set; }

        /// <summary>
        /// Regnmängd (mm).
        /// </summary>
        public double? Rain { get; set; }

        /// <summary>
        /// Molnighet (%).
        /// </summary>
        public double? CloudCover { get; set; }

        /// <summary>
        /// UV-index.
        /// </summary>
        public double? UvIndex { get; set; }

        /// <summary>
        /// Vindhastighet vid 10 m höjd (m/s).
        /// </summary>
        public double? WindSpeed10m { get; set; }

        /// <summary>
        /// Lufttryck vid havsytan (hPa).
        /// </summary>
        public double? PressureMsl { get; set; }

        /// <summary>
        /// Indikator om det är dag (1 = dag, 0 = natt).
        /// </summary>
        public double? IsDay { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Time: {Time}");

            if (Temperature2m.HasValue) sb.Append($", Temp: {Temperature2m.Value:F1}°C");
            if (ApparentTemperature.HasValue) sb.Append($", FeelsLike: {ApparentTemperature.Value:F1}°C");
            if (RelativeHumidity2m.HasValue) sb.Append($", RH: {RelativeHumidity2m.Value:F1}%");
            if (DewPoint2m.HasValue) sb.Append($", DewPt: {DewPoint2m.Value:F1}°C");
            if (Precipitation.HasValue) sb.Append($", Precip: {Precipitation.Value:F1} mm");
            if (Rain.HasValue) sb.Append($", Rain: {Rain.Value:F1} mm");
            if (CloudCover.HasValue) sb.Append($", Cloud: {CloudCover.Value:F1}%");
            if (UvIndex.HasValue) sb.Append($", UV: {UvIndex.Value:F1}");
            if (WindSpeed10m.HasValue) sb.Append($", Wind: {WindSpeed10m.Value:F1} m/s");
            if (PressureMsl.HasValue) sb.Append($", Pressure: {PressureMsl.Value:F1} hPa");
            if (IsDay.HasValue) sb.Append($", IsDay: {(IsDay.Value > 0.5 ? "Yes" : "No")}");

            return sb.ToString();
        }
    }
}
