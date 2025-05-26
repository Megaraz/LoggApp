using System.Text;
using AppLogic.Models.DTOs.Summary;

namespace AppLogic.Models.DTOs.Detailed
{
    public class DayCardDetailed
    {
        public int DayCardId { get; set; }
        public int? UserId { get; set; }
        public DateOnly Date { get; set; }

        public AirQualityDataSummary? AirQualitySummary { get; set; }
        public PollenDataSummary? PollenSummary { get; set; }
        public WeatherDataSummary? WeatherSummary { get; set; }
        public CaffeineDrinkSummary? CaffeineDrinksSummary { get; set; }
        public SupplementSummary? SupplementsSummary { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"\tDate: {Date:yyyy-MM-dd}");

            if (AirQualitySummary?.AirQualityDetails != null && AirQualitySummary.AirQualityDetails.Count > 0)
            {
                sb.AppendLine("\n" + AirQualitySummary.AISummary);
            }
            else
            {
                sb.AppendLine("  [No air quality data]");
            }

            if (PollenSummary?.PollenDataDetails != null && PollenSummary.PollenDataDetails.Count > 0)
            {
                sb.AppendLine("\n" + PollenSummary.AISummary);
            }
            else
            {
                sb.AppendLine("  [No air pollen data]");

            }

            if (WeatherSummary?.WeatherDataDetails != null && WeatherSummary.WeatherDataDetails.Any())
            {
                sb.AppendLine("\n" + WeatherSummary.AISummary);
            }
            else
            {
                sb.AppendLine("  [No weather data]");
            }

            if (CaffeineDrinksSummary != null && CaffeineDrinksSummary.CaffeineDrinksDetails.Count > 0)
            {
                sb.AppendLine($"\n\t\tCaffeinedrinks: {CaffeineDrinksSummary.CaffeineDrinksDetails.Count}");
                sb.AppendLine($"\t\tTotal Caffeine: {CaffeineDrinksSummary.TotalCaffeineInMg}mg");
            }


            return sb.ToString().TrimEnd();
        }

    }
}


