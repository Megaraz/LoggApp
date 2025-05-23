using System.Text;

namespace AppLogic.Models.DTOs
{
    public class DTO_SpecificDayCard
    {
        public int DayCardId { get; set; }
        public int? UserId { get; set; }
        public DateOnly Date { get; set; }

        public DTO_AllAirQualityData? AirQualitySummary { get; set; }
        public DTO_AllPollenData? PollenSummary { get; set; }
        public DTO_AllWeatherData? WeatherSummary { get; set; }
        public DTO_AllCaffeineDrinks? CaffeineDrinksSummary { get; set; }
        public DTO_AllSupplements? SupplementsSummary { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"\tDate: {Date:yyyy-MM-dd}");

            if (AirQualitySummary?.HourlyAirQualityData != null && AirQualitySummary.HourlyAirQualityData.Count > 0)
            {
                
                var avgAQI = AirQualitySummary.HourlyAirQualityData.Average(d => d.AQI.Value ?? 0);

                
                string AQIUnit = AirQualitySummary.HourlyAirQualityData
                    .Select(x => x.AQI.Unit)
                    .FirstOrDefault()!;



                
                sb.AppendLine($"\t\tAvg AQI: {avgAQI:F1} {AQIUnit}");
            }
            else
            {
                sb.AppendLine("  [No air quality data]");
            }

            if (PollenSummary?.HourlyPollenData != null && PollenSummary.HourlyPollenData.Count > 0)
            {
                var avgBirch = PollenSummary.HourlyPollenData.Average(d => d.Birch.Value ?? 0);

                string birchUnit = PollenSummary.HourlyPollenData
                    .Select(x => x.Birch.Unit)
                    .FirstOrDefault()!;

                sb.AppendLine($"\t\tAvg Birch Pollen: {avgBirch:F1} {birchUnit}");
            }

            if (WeatherSummary?.HourlyWeatherData != null && WeatherSummary.HourlyWeatherData.Any())
            {
                var morning = WeatherSummary.HourlyWeatherData
                    .Where(h => h.Time >= 6 && h.Time <= 9) // Exempelvis morgonperiod
                    .ToList();

                


                if (morning.Any())
                {
                    var avgTemp = morning.Average(h => h.Temperature2m.Value ?? 0);
                    var tempUnit = morning
                        .Select(h => h.Temperature2m.Unit)
                        .FirstOrDefault();
                    var avgFeels = morning.Average(h => h.ApparentTemperature.Value ?? 0);
                    var feelTempUnit = morning
                        .Select(h => h.ApparentTemperature.Unit)
                        .FirstOrDefault();
                    var maxWind = morning.Max(h => h.WindSpeed10m.Value ?? 0);
                    var windUnit = morning
                        .Select(h => h.WindSpeed10m.Unit)
                        .FirstOrDefault();

                    sb.AppendLine($"\t\tMorning Avg Temp: {avgTemp:F1} {tempUnit}");
                    sb.AppendLine($"\t\tMorning Feels Like: {avgFeels:F1} {feelTempUnit}");
                    sb.AppendLine($"\t\tMax Wind Speed (6–9): {maxWind:F1} {windUnit}");
                }
                else
                {
                    sb.AppendLine("  [No morning weather data]");
                }
            }
            else
            {
                sb.AppendLine("  [No weather data]");
            }

            if (CaffeineDrinksSummary != null)
            {
                sb.AppendLine($"\n\t\tCaffeinedrinks: {CaffeineDrinksSummary.HourlyCaffeineData.Count}");
                sb.AppendLine($"\t\tTotal Caffeine: {CaffeineDrinksSummary.TotalCaffeineInMg}mg");
            }


            return sb.ToString().TrimEnd();
        }

    }
}


