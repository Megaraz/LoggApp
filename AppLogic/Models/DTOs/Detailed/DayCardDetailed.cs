using System.Text;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Models.DTOs.Detailed
{
    public class DayCardDetailed
    {
        public int DayCardId { get; set; }
        public int UserId { get; set; }
        public DateOnly Date { get; set; }

        public int? TotalCaffeineInMg { get; set; }
        public int? TotalExerciseTime { get; set; }
        public int? TotalCaloriesBurned { get; set; }

        public int? TotalSleepTime { get; set; }

        public AirQualityDataSummary? AirQualitySummary { get; set; }
        public PollenDataSummary? PollenSummary { get; set; }
        public WeatherDataSummary? WeatherSummary { get; set; }
        public List<CaffeineDrinkSummary>? CaffeineDrinksSummaries { get; set; }
        //public SupplementSummary? SupplementsSummary { get; set; }

        public List<ExerciseSummary>? ExercisesSummaries { get; set; }

        public SleepDetailed? SleepDetails { get; set; }


        public DayCardDetailed(DayCard dayCard)
        {
            DayCardId = dayCard.Id;
            UserId = dayCard.UserId;
            Date = dayCard.Date;
            CaffeineDrinksSummaries = dayCard.CaffeineDrinks?.Select(cd => new CaffeineDrinkSummary(cd)).ToList();
            ExercisesSummaries = dayCard.Activities?.OfType<Exercise>()?.Select(e => new ExerciseSummary(e)).ToList();
            SleepDetails = new SleepDetailed(dayCard.Sleep!);
            AirQualitySummary = new AirQualityDataSummary(dayCard.AirQualityData!);
            PollenSummary = new PollenDataSummary(dayCard.AirQualityData!);
            WeatherSummary = new WeatherDataSummary(dayCard.WeatherData!);
            
        }

        public void UpdateTotalValues()
        {
            TotalCaffeineInMg = CaffeineDrinksSummaries?.Sum(x => x.EstimatedMgCaffeine);

            TotalExerciseTime = ExercisesSummaries?
                .Sum(x => (int)(x.Duration?.TotalMinutes ?? 0));

            TotalCaloriesBurned = ExercisesSummaries?.Sum(x => x.ActiveKcalBurned);

            TotalSleepTime = (int)SleepDetails?.TotalSleepTime?.Minutes!;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"\tDate: {Date:yyyy-MM-dd}");

            if (WeatherSummary?.WeatherDataDetails != null && WeatherSummary.WeatherDataDetails.Any())
            {
                sb.AppendLine("\n" + WeatherSummary.AISummary);
            }
            else
            {
                sb.AppendLine("  [No weather data]");
            }

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
            if (SleepDetails != null)
            {
                if (TotalSleepTime.HasValue)
                {
                    sb.AppendLine($"\n\tSleep: {TotalSleepTime}\n");

                }
            }

            if (CaffeineDrinksSummaries != null && CaffeineDrinksSummaries.Count > 0)
            {
                sb.AppendLine($"\n\t\tCaffeinedrinks: {CaffeineDrinksSummaries.Count}");
                sb.AppendLine($"\t\tTotal Caffeine: {TotalCaffeineInMg}mg");
            }

            if (ExercisesSummaries != null && ExercisesSummaries.Count > 0)
            {
                sb.AppendLine($"\n\t\tExercises: {ExercisesSummaries.Count}");

                if (TotalExerciseTime.HasValue)
                {
                    sb.AppendLine($"\t\tTotal Exercise Time: {TotalExerciseTime} minutes");
                }

                if (TotalCaloriesBurned.HasValue)
                {
                    sb.AppendLine($"\t\tTotal Calories Burned: {TotalCaloriesBurned} kcal");
                }
            }


            return sb.ToString().TrimEnd();
        }

    }
}


