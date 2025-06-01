using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Services.Interfaces;

namespace AppLogic.Controllers
{
    /// <summary>
    /// Controller for managing wellness check-ins, including adding, updating, and deleting check-ins associated with day cards.
    /// </summary>
    public class WellnessCheckInController
    {
        private IWellnessCheckInService _wellnessCheckInService;

        public WellnessCheckInController(IWellnessCheckInService wellnessCheckInService)
        {
            _wellnessCheckInService = wellnessCheckInService;
        }

        public async Task<WellnessCheckInDetailed?> AddCheckInToDayCardAsync(int dayCardId, WellnessCheckInInputModel checkInInputModel)
        {
            return await _wellnessCheckInService.AddCheckInToDayCardAsync(dayCardId, checkInInputModel);
        }

        public async Task<bool> DeleteCheckInAsync(int id)
        {
            return await _wellnessCheckInService.DeleteCheckInAsync(id);
        }

        public async Task<WellnessCheckInDetailed?> UpdateCheckInAsync(int id, WellnessCheckInInputModel updateInputModel)
        {
            return await _wellnessCheckInService.UpdateCheckInAsync(id, updateInputModel);
        }
    }
}
