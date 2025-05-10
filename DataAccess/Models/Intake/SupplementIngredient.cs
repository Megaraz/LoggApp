using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models.Intake
{
    public class SupplementIngredient
    {
        public int Id { get; set; }
        public int? SupplementId { get; set; }
        public virtual Supplement? Supplement { get; set; }
        public string? Name { get; set; }
        public int? DosageInMg { get; set; }

        public int? PercentageOfDRI { get; set; } = null;

        public SupplementIngredient(int id, int supplementId, string name, int dosageInMg, int? percentageOfDRI)
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
