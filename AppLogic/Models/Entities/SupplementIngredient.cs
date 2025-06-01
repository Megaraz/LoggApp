namespace AppLogic.Models.Entities
{
    // NOT IMPLEMENTED YET
    public class SupplementIngredient
    {
        public int Id { get; set; }
        public int SupplementId { get; set; }
        public virtual Supplement Supplement { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? DosageInMg { get; set; }

        public int? PercentageOfDRI { get; set; } = null;

        public SupplementIngredient(int supplementId, string name, int dosageInMg, int? percentageOfDRI)
        {
            SupplementId = supplementId;
            Name = name;
            DosageInMg = dosageInMg;
            PercentageOfDRI = percentageOfDRI;
        }

        public SupplementIngredient()
        {
        }
    }
}
