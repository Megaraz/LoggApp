namespace AppLogic.Models.InputModels
{
    public class UserInputModel
    {
        public string Username { get; set; }
        public string? CityName { get; set; }
        private readonly string _countryCode = "SE";

        public GeoResult? GeoResult { get; set; }

        public UserInputModel(string username)
        {
            Username = username;
        }

    }
}
