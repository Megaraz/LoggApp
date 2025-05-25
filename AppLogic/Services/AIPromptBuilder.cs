using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Weather;
using AppLogic.Models.Weather.AirQuality;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Services
{
    public static class AiPromptBuilder
    {
        public static string BuildWeatherPrompt(WeatherData weather)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Du är meteorolog. Skriv EN mening på svenska som sammanfattar vädret för dagen.\r\n\r\nData:");

            var block = weather.HourlyBlock;
            var units = weather.HourlyUnits;

            if (block == null || units == null) return "Saknar väderdata.";

            sb.AppendLine("Väderdata per timme:");
            sb.AppendLine($"Tid\tTemp({units.Temperature2m})\tFeelsLike({units.ApparentTemperature})\tHumidity({units.RelativeHumidity2m})\tPrecip({units.Precipitation})\tRain({units.Rain})\tCloud({units.CloudCover})\tUV({units.UvIndex})\tWind({units.WindSpeed10m})\tPressure({units.PressureMsl})");

            int count = block.Time.Count;
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"{block.Time[i].Hour:00}:00\t" +
                    $"{block.Temperature2m[i]:0.#}\t{block.ApparentTemperature[i]:0.#}\t{block.RelativeHumidity2m[i]:0.#}\t" +
                    $"{block.Precipitation[i]:0.#}\t{block.Rain[i]:0.#}\t{block.CloudCover[i]:0.#}\t{block.UvIndex[i]:0.#}\t" +
                    $"{block.WindSpeed10m[i]:0.#}\t{block.PressureMsl[i]:0.#}");
            }

            return sb.ToString();
        }

        public static string BuildAirQualityPrompt(AirQualityData airQuality)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Du är meteorolog. Skriv EN mening på svenska som sammanfattar luftkvalitéten för dagen.\r\n\r\nData:");
            var block = airQuality.HourlyBlock;
            var units = airQuality.HourlyUnits;

            if (block == null || units == null) return "Saknar luftkvalitetsdata.";

            sb.AppendLine("Luftkvalitetsdata per timme:");
            sb.AppendLine($"Tid\tUVI({units.UVI})\tAQI({units.AQI})\tPM2.5({units.PM25})\tOzon({units.Ozone})\tCO({units.CO})\tNO2({units.NO2})\tDust({units.Dust})");

            int count = block.Time.Count;
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"{block.Time[i].Hour:00}:00\t" +
                    $"{block.UVI[i]:0.#}\t{block.AQI[i]:0.#}\t{block.PM25[i]:0.#}\t{block.Ozone[i]:0.#}\t" +
                    $"{block.CarbonMonoxide[i]:0.#}\t{block.NitrogenDioxide[i]:0.#}\t{block.Dust[i]:0.#}");
            }

            return sb.ToString();
        }

        public static string BuildPollenPrompt(AirQualityData airQuality)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Du är meteorolog. Skriv EN mening på svenska som sammanfattar pollen för dagen.\r\n\r\nData:");
            var block = airQuality.HourlyBlock;
            var units = airQuality.HourlyUnits;

            if (block == null || units == null) return "Saknar pollendata.";

            sb.AppendLine("Pollendata (grains/m3) per timme:");
            sb.AppendLine($"Tid\tBjörk({units.BirchPollen})\tAl({units.AlderPollen})\tGräs({units.GrassPollen})\tMalört({units.MugwortPollen})\tAmbrosia({units.RagweedPollen})");

            int count = block.Time.Count;
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"{block.Time[i].Hour:00}:00\t" +
                    $"{block.BirchPollen[i]:0.#}\t{block.AlderPollen[i]:0.#}\t{block.GrassPollen[i]:0.#}\t" +
                    $"{block.MugwortPollen[i]:0.#}\t{block.RagweedPollen[i]:0.#}");
            }

            return sb.ToString();
        }
    }


}
