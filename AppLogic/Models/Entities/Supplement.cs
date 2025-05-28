using AppLogic.Interfaces;

namespace AppLogic.Models.Entities
{
    public class Supplement : ITimeOfEntry, IDailyLogId
    {

        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard DayCard { get; set; } = null!;
        public string Name { get; set; } = null!;
        public TimeOnly? TimeOf { get; private set; }
        public virtual ICollection<SupplementIngredient>? Ingredients { get; set; } = new List<SupplementIngredient>();

        public Supplement(int dailyCardId, string name, TimeOnly timeOf, List<SupplementIngredient> ingredients)
        {
            DayCardId = dailyCardId;
            Name = name;
            TimeOf = timeOf;
            Ingredients = ingredients;
        }

        public Supplement()
        {
        }
    }
}
