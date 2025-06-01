namespace AppLogic.Interfaces
{
    /// <summary>
    /// Interface for entities that have a time of entry property.
    /// </summary>
    internal interface ITimeOfEntry
    {
        TimeOnly? TimeOf { get; }
    }
}
