using AppLogic.Models.Enums;

namespace AppLogic.Models.Entities
{
    public class Exercise : Activity
    {
        public ExerciseType? ExerciseType { get; set; }
        public PerceivedIntensity? PerceivedIntensity { get; set; }
        public int? TrainingLoad { get; set; }
        public int? AvgHeartRate { get; set; }
        public CLOCK_Intensity? Intensity { get; set; }
        public int? ActiveKcalBurned { get; set; }
        public int? Distance { get; set; }
        public int? AvgKmTempo { get; set; }
        public int? Steps { get; set; }
        public int? AvgStepLength { get; set; }
        public int? AvgStepPerMin { get; set; }
    }
}
