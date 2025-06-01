using System.ComponentModel.DataAnnotations.Schema;
using AppLogic.Models.Entities.WeatherAndAQI;

namespace AppLogic.Models.Entities
{
    /// <summary>
    /// Entity representing a day card, which contains various health and wellness metrics for a specific date.
    /// </summary>
    public class DayCard
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Exercise>? Exercises { get; set; } = new List<Exercise>();
        public virtual ICollection<CaffeineDrink>? CaffeineDrinks { get; set; } = new List<CaffeineDrink>();
        public virtual ICollection<WellnessCheckIn>? WellnessCheckIns { get; set; } = new List<WellnessCheckIn>();
        public virtual Sleep? Sleep { get; set; }
        public virtual WeatherData? WeatherData { get; set; }
        public virtual AirQualityData? AirQualityData { get; set; }


        public DayCard()
        {
        }

    }
}
