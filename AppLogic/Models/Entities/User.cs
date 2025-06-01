using AppLogic.Models.InputModels;

namespace AppLogic.Models.Entities
{
    /// <summary>
    /// Entity representing a user in the application.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public virtual ICollection<DayCard>? DayCards { get; set; } = new List<DayCard>();
        public string? CityName { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }

        public User(UserInputModel userInputModel)
        {
            Username = userInputModel.Username;
            CityName = userInputModel.GeoResult?.Name ?? userInputModel.CityName;
            Lat = userInputModel.GeoResult?.Lat;
            Lon = userInputModel.GeoResult?.Lon;
        }

        public User()
        {
            
        }

    }
}
