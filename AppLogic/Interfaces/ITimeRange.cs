namespace AppLogic.Interfaces
{
    internal interface ITimeRange : ITimeOfEntry
    {
        TimeOnly? EndTime { get; }
        TimeSpan? Duration { get; }
    }
}
