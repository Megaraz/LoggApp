using System.ComponentModel.DataAnnotations.Schema;
using AppLogic.Models.Entities.WeatherAndAQI;

namespace AppLogic.Models.Entities
{
    public class DayCard
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Activity>? Activities { get; set; } = new List<Activity>();
        public virtual ICollection<CaffeineDrink>? CaffeineDrinks { get; set; } = new List<CaffeineDrink>();
        public virtual ICollection<Supplement>? Supplements { get; set; } = new List<Supplement>();
        public virtual ICollection<WellnessCheckIn>? WellnessCheckIns { get; set; } = new List<WellnessCheckIn>();
        public virtual Sleep? Sleep { get; set; }
        public virtual WeatherData? WeatherData { get; set; }
        public virtual AirQualityData? AirQualityData { get; set; }

        [NotMapped]
        public IEnumerable<Exercise> Exercises => Activities?.OfType<Exercise>() ?? Enumerable.Empty<Exercise>();


        public DayCard()
        {
        }

    }
}
