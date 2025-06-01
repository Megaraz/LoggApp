using AppLogic.Models.Enums;
using AppLogic.Models.InputModels;

namespace AppLogic.Models.Entities
{
    /// <summary>
    /// Entity representing an exercise activity associated with a specific day card.
    /// </summary>
    public class Exercise : Activity
    {
        public ExerciseType? ExerciseType { get; set; }
        public PerceivedIntensity? PerceivedIntensity { get; set; }
        public int? TrainingLoad { get; set; }
        public int? AvgHeartRate { get; set; }
        public int? ActiveKcalBurned { get; set; }
        public double? DistanceInKm { get; set; }
        public TimeSpan? AvgKmTempo { get; set; }
        public int? Steps { get; set; }
        public int? AvgStepLengthInCm { get; set; }
        public int? AvgStepPerMin { get; set; }




        public Exercise()
        {
        }

        public Exercise(int dayCardId, ExerciseInputModel inputModel)
        {
            base.DayCardId = dayCardId;
            base.TimeOf = inputModel.TimeOf;
            base.EndTime = inputModel.EndTime;
            base.Duration = inputModel.Duration;
            ExerciseType = inputModel.ExerciseType;
            PerceivedIntensity = inputModel.PerceivedIntensity;
            TrainingLoad = inputModel.TrainingLoad;
            AvgHeartRate = inputModel.AvgHeartRate;
            ActiveKcalBurned = inputModel.ActiveKcalBurned;
            DistanceInKm = inputModel.DistanceInKm;
            AvgKmTempo = inputModel.AvgKmTempo;
            Steps = inputModel.Steps;
            AvgStepLengthInCm = inputModel.AvgStepLengthInCm;
            AvgStepPerMin = inputModel.AvgStepPerMin;
        }


    }

}
