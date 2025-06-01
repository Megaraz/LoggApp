using AppLogic.Models.Enums;
using AppLogic.Models.InputModels;

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
            Intensity = inputModel.ClockIntensity;
            ActiveKcalBurned = inputModel.ActiveKcalBurned;
            Distance = inputModel.Distance;
            AvgKmTempo = inputModel.AvgKmTempo;
            Steps = inputModel.Steps;
            AvgStepLength = inputModel.AvgStepLength;
            AvgStepPerMin = inputModel.AvgStepPerMin;
        }


        public Exercise(
            int dayCardId, 
            TimeOnly? timeOf, 
            TimeOnly? endTime, 
            TimeSpan? duration,
            ExerciseType? exerciseType, 
            PerceivedIntensity? perceivedIntensity, 
            int? trainingLoad, 
            int? avgHeartRate, 
            CLOCK_Intensity? intensity, 
            int? activeKcalBurned, 
            int? distance, 
            int? avgKmTempo, 
            int? steps, 
            int? avgStepLength, 
            int? avgStepPerMin) : base(dayCardId, timeOf, endTime, duration)
        {
            ExerciseType = exerciseType;
            PerceivedIntensity = perceivedIntensity;
            TrainingLoad = trainingLoad;
            AvgHeartRate = avgHeartRate;
            Intensity = intensity;
            ActiveKcalBurned = activeKcalBurned;
            Distance = distance;
            AvgKmTempo = avgKmTempo;
            Steps = steps;
            AvgStepLength = avgStepLength;
            AvgStepPerMin = avgStepPerMin;
        }

    }

}
