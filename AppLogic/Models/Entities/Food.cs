using AppLogic.Interfaces;
using AppLogic.Models.Enums;

namespace AppLogic.Models.Entities
{
    // NOT IMPLEMENTED YET
    public class Food : ITimeOfEntry, IDailyLogId
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard DayCard { get; set; } = null!;
        public TimeOnly? TimeOf { get; set; }
        public int? EstimatedKcal { get; set; }
        public int? EstimatedProteinInGrams { get; set; }
        FoodType? FoodType { get; set; }

        public Food(int dayCardId, TimeOnly timeOf, int estimatedKcal, int? estimatedProteinInGrams, FoodType foodType)
        {
            DayCardId = DayCardId;
            TimeOf = timeOf;
            EstimatedKcal = estimatedKcal;
            EstimatedProteinInGrams = estimatedProteinInGrams;
            FoodType = foodType;
        }

        public Food()
        {
        }
    }
}
