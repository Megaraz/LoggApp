namespace AppLogic.Models.DTOs.Summary
{
    public class DayCardSummary
    {
        public int DayCardId { get; set; }
        public int? UserId { get; set; }
        public DateOnly? Date { get; set; }

        public int? Entries { get; set; }

        public override string ToString()
        {
            return $"[{DayCardId}]\t\t{Date}\t{Entries}";
        }
    }
}
