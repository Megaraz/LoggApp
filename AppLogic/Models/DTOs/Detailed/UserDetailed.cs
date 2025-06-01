using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;

namespace AppLogic.Models.DTOs.Detailed
{
    /// <summary>
    /// DTO that epresents detailed information about a user, including their ID, username, city, geographical coordinates, and associated day cards.
    /// </summary>
    public class UserDetailed
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? CityName { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int? DayCardCount { get; set; }

        public List<DayCardSummary>? AllDayCardsSummary { get; set; }
        public string? SleepLastWeek { get; internal set; }
        public string? WellnessLastWeek { get; internal set; }
        public string? ExerciseLastWeek { get; internal set; }
        public string? CaffeineLastWeek { get; internal set; }

        public UserDetailed(User user)
        {
            Id = user!.Id;
            Username = user.Username!;
            CityName = user.CityName;
            Lat = user.Lat;
            Lon = user.Lon;
            AllDayCardsSummary = user.DayCards!
                .Select(dayCard => new DayCardSummary(dayCard)).ToList();
            DayCardCount = user.DayCards?.Count ?? 0;
        }

        public UserDetailed()
        {
            
        }


        public override string ToString()
        {
            return $"[{Id.ToString()}]" + string.Empty.PadRight(3) +
                $"{Username?.PadRight(12)}{CityName?.PadRight(15)}{DayCardCount?.ToString()}\n\n" +
                "STATS OVERVIEW LAST WEEK:\n\n" +
                $"Sleep: {SleepLastWeek}\nWellness: {WellnessLastWeek}\n" +
                $"Exercise: {ExerciseLastWeek}\nCaffeine: {CaffeineLastWeek}";
        }
    }
}
