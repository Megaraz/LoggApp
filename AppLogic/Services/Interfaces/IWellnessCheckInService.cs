using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;

namespace AppLogic.Services.Interfaces
{
    /// <summary>
    /// Service for managing wellness check-ins, including adding, updating, and deleting check-ins associated with day cards.
    /// </summary>
    public interface IWellnessCheckInService
    {
        Task<WellnessCheckInDetailed> AddCheckInToDayCardAsync(int dayCardId, WellnessCheckInInputModel checkInInputModel);
        Task<bool> DeleteCheckInAsync(int checkInId);
        Task<WellnessCheckInDetailed> UpdateCheckInAsync(int checkInId, WellnessCheckInInputModel updateInputModel);
    }
}
