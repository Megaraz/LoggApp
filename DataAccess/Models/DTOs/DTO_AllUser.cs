namespace AppLogic.Models.DTOs
{
    public class DTO_AllUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int DayCardCount { get; set; }
        public string? CityName { get; set; }

        public override string ToString()
        {
            return $"[{Id}]. {Username}, {CityName}"
                + $"´Daycards: {DayCardCount}";
        }
    }
}
