using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Models.Weather.AirQuality
{
    public class HourlyAirQualityEntry
    {
        /// <summary>
        /// Tidpunkten för mätningen.
        /// </summary>
        public DateTime Time { get; set; }

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
    }
}
