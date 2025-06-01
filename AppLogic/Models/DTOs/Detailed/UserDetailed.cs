using AppLogic.Models.DTOs.Summary;

namespace AppLogic.Models.DTOs.Detailed
{
    public class UserDetailed
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? CityName { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }

        public int? DayCardCount { get; set; }

        public List<DayCardSummary>? AllDayCardsSummary { get; set; }


        public override string ToString()
        {
            return $"[{Id.ToString()}]" + string.Empty.PadRight(3) +
                $"{Username?.PadRight(12)}{CityName?.PadRight(15)}{DayCardCount?.ToString()}";
        }
    }
}
