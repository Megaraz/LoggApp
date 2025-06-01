using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing sleep records associated with day cards.
    /// </summary>
    public interface ISleepRepo : IGenericRepo<Sleep>
    {
        Task<Sleep> UpdateSleepAsync(Sleep sleep);
    }
}
