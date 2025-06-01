using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;

namespace AppLogic.Models.DTOs.Summary
{
    /// <summary>
    /// DTO that represents a summary of a day card, including the number of entries and the date.
    /// </summary>
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

            Entries = (dayCard.Exercises?.Count ?? 0) + 
                (dayCard.CaffeineDrinks?.Count ?? 0) +
                (dayCard.WellnessCheckIns?.Count ?? 0) +
                (dayCard.Sleep != null ? 1 : 0);
        }

        public DayCardSummary(DayCardDetailed dayCardDetailed)
        {
            UserId = dayCardDetailed.UserId;
            DayCardId = dayCardDetailed.DayCardId;
            Date = dayCardDetailed.Date;
            Entries =   (dayCardDetailed.CaffeineDrinksSummaries?.Count ?? 0) +
                        (dayCardDetailed.WellnessCheckInsSummaries?.Count ?? 0) +
                        (dayCardDetailed.ExercisesSummaries?.Count ?? 0) +
                        (dayCardDetailed.SleepDetails != null ? 1 : 0);
        }


        public override string ToString()
        {
            return $"[{DayCardId}]\t\t{Date}\t{Entries}";
        }
    }
}
