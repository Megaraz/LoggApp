using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppLogic.Models.DTOs.Summary
{
    public class UserSummary
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? CityName { get; set; }
        public int DayCardCount { get; set; }




        public override string ToString()
        {

            return $"[{Id.ToString()}]" + string.Empty.PadRight(3) +
                $"{Username.PadRight(12)}{CityName?.PadRight(15)}{DayCardCount}";
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
