using System.Text;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppLogic.Models.DTOs.Detailed
{
    /// <summary>
    /// DTO that represents a detailed view of a day card, including various health and wellness metrics.
    /// </summary>
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

        public List<WellnessCheckInSummary> WellnessCheckInsSummaries { get; set; }

        //public SupplementSummary? SupplementsSummary { get; set; }

        public List<ExerciseSummary>? ExercisesSummaries { get; set; }

        public SleepDetailed? SleepDetails { get; set; }


        public DayCardDetailed(DayCard dayCard)
        {
            DayCardId = dayCard.Id;
            UserId = dayCard.UserId;
            Date = dayCard.Date;
            WellnessCheckInsSummaries = dayCard.WellnessCheckIns?
                .Select(wc => new WellnessCheckInSummary(wc)).ToList() ?? new List<WellnessCheckInSummary>();
            CaffeineDrinksSummaries = dayCard.CaffeineDrinks?.Select(cd => new CaffeineDrinkSummary(cd)).ToList();
            ExercisesSummaries = dayCard.Exercises?.OfType<Exercise>()?.Select(e => new ExerciseSummary(e)).ToList();
            SleepDetails = dayCard.Sleep == null ? null : new SleepDetailed(dayCard.Sleep);
            AirQualitySummary = dayCard.AirQualityData == null ? null : new AirQualityDataSummary(dayCard.AirQualityData!);
            PollenSummary = dayCard.AirQualityData == null ? null : new PollenDataSummary(dayCard.AirQualityData!);
            WeatherSummary = dayCard.WeatherData == null ? null : new WeatherDataSummary(dayCard.WeatherData!);


        }

        public void UpdateTotalValues()
        {
            TotalCaffeineInMg = CaffeineDrinksSummaries?.Sum(x => x.EstimatedMgCaffeine);

            TotalExerciseTime = ExercisesSummaries?
                .Sum(x => (int)(x.Duration?.TotalMinutes ?? 0));

            TotalCaloriesBurned = ExercisesSummaries?.Sum(x => x.ActiveKcalBurned);
            if (SleepDetails != null && SleepDetails.TotalSleepTime != null)
            {
                TotalSleepTime = (int)SleepDetails?.TotalSleepTime?.Minutes!;
            }
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
                if (SleepDetails.TotalSleepTime.HasValue)
                {
                    TotalSleepTime = (int)SleepDetails.TotalSleepTime.Value.TotalMinutes;
                    var totalSleep = SleepDetails.TotalSleepTime.Value;
                    sb.AppendLine($"\n\tSleep Stats: ");
                    sb.Append($"\t\tTotal Hours Slept: {totalSleep.Hours}h");
                    if (totalSleep.Minutes > 0)
                    {
                        sb.Append($" {totalSleep.Minutes}min");
                    }
                    sb.AppendLine();
                }
            }

            if (CaffeineDrinksSummaries != null && CaffeineDrinksSummaries.Count > 0)
            {
                sb.AppendLine($"\n\tCaffeinedrinks: {CaffeineDrinksSummaries.Count}");
                sb.AppendLine($"\t\tTotal Caffeine: {TotalCaffeineInMg}mg");
            }

            if (ExercisesSummaries != null && ExercisesSummaries.Count > 0)
            {
                sb.AppendLine($"\n\tTimes Exercised: {ExercisesSummaries.Count}");

                if (TotalExerciseTime.HasValue)
                {
                    sb.AppendLine($"\t\tTotal Exercise Time: {TotalExerciseTime} minutes");
                }

                if (TotalCaloriesBurned.HasValue && TotalCaloriesBurned > 0)
                {
                    sb.AppendLine($"\t\tTotal Calories Burned: {TotalCaloriesBurned} kcal");
                }
            }

            if (WellnessCheckInsSummaries != null && WellnessCheckInsSummaries.Count > 0)
            {
                sb.AppendLine($"\n\tWellness Check-Ins: {WellnessCheckInsSummaries.Count}");


                if (WellnessCheckInsSummaries.Any(wc => wc.EnergyLevel.HasValue))
                {
                    sb.Append($"\t\tAvg Energy: ");
                    int averageEnergy = (int)Math.Round(WellnessCheckInsSummaries
                        .Where(wc => wc.EnergyLevel.HasValue)
                        .Average(wc => (int)wc.EnergyLevel!.Value), MidpointRounding.AwayFromZero);

                    switch (averageEnergy)
                    {
                        case 0:
                            sb.AppendLine("Very Low");
                            break;
                        case 1:
                            sb.AppendLine("Low");
                            break;
                        case 2:
                            sb.AppendLine("Neutral");
                            break;
                        case 3:
                            sb.AppendLine("Good");
                            break;
                        case 4:
                            sb.AppendLine("Very Good");
                            break;
                        case 5:
                            goto case 4; // Avg can reach 5 so need this aswell

                        default:
                            sb.AppendLine("Unknown Energy Level");
                            break;
                    }
                    //sb.AppendLine($"{averageEnergy}");

                }
                else
                {
                    sb.AppendLine("No energy levels recorded.");
                }


                if (WellnessCheckInsSummaries.Any(wc => wc.MoodLevel.HasValue))
                {
                    sb.Append($"\t\tAvg Mood: ");
                    int averageMood = (int)Math.Round(WellnessCheckInsSummaries
                        .Where(wc => wc.MoodLevel.HasValue)
                        .Average(wc => (int)wc.MoodLevel!.Value), MidpointRounding.AwayFromZero);
                    
                    switch(averageMood)
                    {
                        case 0:
                            sb.Append("Very Low");
                            break;
                        case 1:
                            sb.Append("Low");
                            break;
                        case 2:
                            sb.Append("Neutral");
                            break;
                        case 3:
                            sb.Append("Good");
                            break;
                        case 4:
                            sb.Append("Very Good");
                            break;
                        case 5:
                            goto case 4; // Avg can reach 5 so need this aswell

                        default:
                            sb.Append("Unknown Mood Level");
                            break;
                    }
                    sb.AppendLine();
                    //sb.AppendLine($"{averageMood}");
                }
                else
                {
                    sb.AppendLine("No mood levels recorded.");
                }
            }


            return sb.ToString().TrimEnd();
        }

    }
}


