using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Presentation;

namespace BusinessLogic.Models
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
            CityName = userInputModel.GeoResult.Name;
            Lat = userInputModel.GeoResult.Lat;
            Lon = userInputModel.GeoResult.Lon;
        }

        public User()
        {
            
        }


        public override string ToString()
        {
            return $"{this.Username}, ID: {this.Id}\n"
                + $"{this.CityName} \n"
                + $"DayCards: {this.DayCards!.Count}\n\n"
                + "------------------------\n"
                + $"[DAYCARDS] for [{this.Username}]\n"
                + $"{string.Join("\n", DayCards?.Select(x => x.Date)!)}"
                + "------------------------\n";   
        }
    }
}
