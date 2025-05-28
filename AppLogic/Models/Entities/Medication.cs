using AppLogic.Interfaces;

namespace AppLogic.Models.Entities
{
    public class Medication : ITimeOfEntry, IDailyLogId
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard DayCard { get; set; } = null!;
        public TimeOnly? TimeOf { get; }
        public string Name { get; } = null!;
        public int? DosageInMg { get; }
        public string? PrescribedFor { get; }

        public Medication(TimeOnly timeOf, string name, int dosageInMg, string? prescribedFor)
        {
            DosageInMg = dosageInMg;
            TimeOf = timeOf;
            Name = name;
            PrescribedFor = prescribedFor;
        }

        public Medication()
        {
        }

        
    }
}
