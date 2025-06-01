namespace AppLogic.Interfaces
{
    /// <summary>
    /// Interface for entities that represent a time range, including a start time and an optional end time or duration.
    /// </summary>
    internal interface ITimeRange : ITimeOfEntry
    {
        TimeOnly? EndTime { get; }
        TimeSpan? Duration { get; }
    }
}
