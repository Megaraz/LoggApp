namespace AppLogic.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public virtual ICollection<DayCard>? DayCards { get; set; } = new List<DayCard>();
        public string? CityName { get; set; }
        public string? CountryCode { get; set; } = "SE";
        public double? Lat { get; set; }
        public double? Lon { get; set; }

        public User(UserInputModel userInputModel)
        {
            Username = userInputModel.Username;
            CityName = userInputModel.GeoResult?.Name ?? userInputModel.CityName;
            Lat = userInputModel.GeoResult.Lat;
            Lon = userInputModel.GeoResult.Lon;
        }

        public User()
        {
            
        }


        //public override string ToString()
        //{
        //    return $"{Username}, ID: {Id}\n"
        //        + $"{CityName} \n"
        //        + $"DayCards: {DayCards!.Count}\n\n"
        //        + "------------------------\n"
        //        + $"[DAYCARDS] for [{Username}]\n"
        //        + $"{string.Join("\n", DayCards?.Select(x => x.Date)!)}"
        //        + "------------------------\n";   
        //}
    }
}
