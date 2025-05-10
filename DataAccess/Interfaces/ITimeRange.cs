using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    internal interface ITimeRange : ITimeOfEntry
    {
        TimeOnly? EndTime { get; }
        TimeSpan? Duration { get; }
    }
}
