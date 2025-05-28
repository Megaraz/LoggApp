using AppLogic.Interfaces;

namespace AppLogic.Models.Entities
{
    public abstract class Activity : ITimeOfEntry, ITimeRange, IDailyLogId
    {
        public int Id { get; set; }
        public int DayCardId { get; set; }
        public virtual DayCard DayCard { get; set; } = null!;
        public TimeOnly? TimeOf { get; set; }
        public TimeOnly? EndTime { get; set; }

        private TimeSpan? _duration;

        public TimeSpan? Duration
        {
            get => _duration ?? (TimeOf.HasValue && EndTime.HasValue ? EndTime - TimeOf : null);
            set => _duration = value;
        }

        protected Activity()
        {
            
        }

        protected Activity(int dayCardId, TimeOnly? timeOf, TimeOnly? endTime, TimeSpan? duration)
        {
            DayCardId = dayCardId;
            TimeOf = timeOf;
            EndTime = endTime;
            Duration = duration;
        }

    }
}
