using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;

namespace AppLogic.Models.DTOs.Summary
{
    /// <summary>
    /// DTO for summarizing user information, including the user's ID, username, city name, and the count of day cards associated with the user.
    /// </summary>
    public class UserSummary
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? CityName { get; set; }
        public int DayCardCount { get; set; }


        public UserSummary(UserDetailed userDetailed)
        {
            Id = userDetailed.Id;
            Username = userDetailed.Username;
            CityName = userDetailed.CityName;
            DayCardCount = userDetailed.AllDayCardsSummary!.Count;
        }

        public UserSummary(User user)
        {
            Id = user.Id;
            Username = user.Username;
            CityName = user.CityName;
            DayCardCount = user.DayCards?.Count ?? 0;
        }

        public UserSummary()
        {
            
        }


        public override string ToString()
        {

            return $"[{Id.ToString()}]" + string.Empty.PadRight(3) +
                $"{Username?.PadRight(12)}{CityName?.PadRight(15)}{DayCardCount}";
        }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();

        //    sb.AppendLine($"[{Id}]. {Username}, {CityName}"
        //        + $", Daycards: {DayCardCount}");

        //    return sb.ToString().TrimEnd();
        //}
    }
}
