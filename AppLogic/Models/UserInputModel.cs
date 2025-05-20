namespace AppLogic.Models
{
    public class UserInputModel
    {
        public string Username { get; set; }
        public string CityName { get; set; }
        private readonly string _countryCode = "SE";

        public GeoResult GeoResult { get; set; }
    }
}
