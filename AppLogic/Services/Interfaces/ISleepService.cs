using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.InputModels;

namespace AppLogic.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing sleep data, including adding, updating, and deleting sleep records associated with day cards.
    /// </summary>
    public interface ISleepService
    {
        Task<SleepDetailed> AddSleepToDayCardAsync(int dayCardId, SleepInputModel sleepInputModel);
        Task<bool> DeleteSleepAsync(int id);
        Task<SleepDetailed?> UpdateSleepAsync(int id, SleepInputModel sleepInputModel);
    }
}
