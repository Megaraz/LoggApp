using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.Entities;

namespace AppLogic.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing wellness check-ins associated with day cards.
    /// </summary>
    public interface IWellnessCheckInRepo : IGenericRepo<WellnessCheckIn>
    {
        Task<WellnessCheckIn> UpdateCheckInAsync(WellnessCheckIn wellnessCheckIn);
    }
}
