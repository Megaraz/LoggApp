using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;

namespace AppLogic.Models.DTOs.Summary
{
    public class DayCardSummary
    {
        public int DayCardId { get; set; }
        public int UserId { get; set; }
        public DateOnly Date { get; set; }

        public int? Entries { get; set; }

        public DayCardSummary(DayCard dayCard)
        {
            DayCardId = dayCard.Id;
            UserId = dayCard.UserId;
            Date = dayCard.Date;

            Entries = (dayCard.Activities!.Count + dayCard.CaffeineDrinks!.Count + dayCard.Supplements!.Count);
        }

        public DayCardSummary(DayCardDetailed dayCardDetailed)
        {
            UserId = dayCardDetailed.UserId;
            DayCardId = dayCardDetailed.DayCardId;
            Date = dayCardDetailed.Date;
            Entries = (dayCardDetailed.CaffeineDrinksSummaries?.Count ?? 0) +
                      (dayCardDetailed.ExercisesSummaries?.Count ?? 0) +
                      (dayCardDetailed.SleepDetails != null ? 1 : 0) +
                      (dayCardDetailed.AirQualitySummary != null ? 1 : 0) +
                      (dayCardDetailed.PollenSummary != null ? 1 : 0) +
                      (dayCardDetailed.WeatherSummary != null ? 1 : 0);
        }


        public override string ToString()
        {
            return $"[{DayCardId}]\t\t{Date}\t{Entries}";
        }
    }
}
