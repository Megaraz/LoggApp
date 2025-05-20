using AppLogic.Interfaces;

namespace AppLogic.Models.Intake
{
    public class Medication : ITimeOfEntry, IDailyLogId
    {
        public int Id { get; set; }
        public int? DayCardId { get; set; }
        public virtual DayCard? DayCard { get; set; }
        public TimeOnly? TimeOf { get; }
        public string? NameOfMedication { get; }
        public int? DosageInMg { get; }
        public string? PrescribedFor { get; }

        public Medication(TimeOnly timeOf, string nameOfMedication, int dosageInMg, string? prescribedFor)
        {
            DosageInMg = dosageInMg;
            TimeOf = timeOf;
            NameOfMedication 
                = string.IsNullOrEmpty(nameOfMedication) 
                ? throw new ArgumentException("Medication name is required.") 
                : nameOfMedication;
            PrescribedFor = prescribedFor;
        }

        public Medication()
        {
        }

        //public MedicationLog UpdateDosage(int newDosage)
        //{
        //    if (newDosage <= 0)
        //        throw new ArgumentOutOfRangeException(nameof(newDosage)); 

        //    return new MedicationLog(TimeOf, NameOfMedication, newDosage, PrescribedFor);



        //}

    }
}
