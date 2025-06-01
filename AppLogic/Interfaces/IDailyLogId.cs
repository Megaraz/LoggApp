using AppLogic.Models;

namespace AppLogic.Interfaces
{
    /// <summary>
    /// Interface for entities that have a daily log ID, typically used for day cards or similar records.
    /// </summary>
    public interface IDailyLogId
    {
        public int DayCardId { get; set; }
        
    }
}
