using System.Text;

namespace AppLogic.Models.Weather.AirQuality
{
    public class HourlyAirQualityData
    {
        /// <summary>
        /// Tidpunkten för mätningen.
        /// </summary>
        public int? Time { get; set; }

        /// <summary>
        /// Björkpollen (antal korn per m³).
        /// </summary>
        public double? BirchPollen { get; set; }

        /// <summary>
        /// Alarpollen (antal korn per m³).
        /// </summary>
        public double? AlderPollen { get; set; }

        /// <summary>
        /// Gräspollen (antal korn per m³).
        /// </summary>
        public double? GrassPollen { get; set; }

        /// <summary>
        /// Beppelpollen (antal korn per m³).
        /// </summary>
        public double? MugwortPollen { get; set; }

        /// <summary>
        /// Ambrosiapollen (antal korn per m³).
        /// </summary>
        public double? RagweedPollen { get; set; }

        /// <summary>
        /// UV-index.
        /// </summary>
        public double? UVI { get; set; }

        /// <summary>
        /// Europeiskt luftkvalitetsindex (AQI).
        /// </summary>
        public double? AQI { get; set; }

        /// <summary>
        /// Partiklar ≤2.5 µm (µg/m³).
        /// </summary>
        public double? PM25 { get; set; }

        /// <summary>
        /// Ozon (µg/m³).
        /// </summary>
        public double? Ozone { get; set; }

        /// <summary>
        /// Kolmonoxid (µg/m³).
        /// </summary>
        public double? CarbonMonoxide { get; set; }

        /// <summary>
        /// Kvävedioxid (µg/m³).
        /// </summary>
        public double? NitrogenDioxide { get; set; }

        /// <summary>
        /// Damm (µg/m³).
        /// </summary>
        public double? Dust { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Time: {Time}");

            if (BirchPollen.HasValue) sb.Append($", Birch: {BirchPollen.Value:F1}");
            if (AlderPollen.HasValue) sb.Append($", Alder: {AlderPollen.Value:F1}");
            if (GrassPollen.HasValue) sb.Append($", Grass: {GrassPollen.Value:F1}");
            if (MugwortPollen.HasValue) sb.Append($", Mugwort: {MugwortPollen.Value:F1}");
            if (RagweedPollen.HasValue) sb.Append($", Ragweed: {RagweedPollen.Value:F1}");
            if (UVI.HasValue) sb.Append($", UVI: {UVI.Value:F1}");
            if (AQI.HasValue) sb.Append($", AQI: {AQI.Value:F1}");
            if (PM25.HasValue) sb.Append($", PM2.5: {PM25.Value:F1}");
            if (Ozone.HasValue) sb.Append($", O₃: {Ozone.Value:F1}");
            if (CarbonMonoxide.HasValue) sb.Append($", CO: {CarbonMonoxide.Value:F1}");
            if (NitrogenDioxide.HasValue) sb.Append($", NO₂: {NitrogenDioxide.Value:F1}");
            if (Dust.HasValue) sb.Append($", Dust: {Dust.Value:F1}");

            return sb.ToString();
        }
    }
}
