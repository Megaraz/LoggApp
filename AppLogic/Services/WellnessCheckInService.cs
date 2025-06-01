using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    /// <summary>
    /// Service for managing wellness check-ins, including adding, updating, and deleting check-ins associated with day cards.
    /// </summary>
    public class WellnessCheckInService : IWellnessCheckInService
    {
        private readonly IWellnessCheckInRepo _wellnessCheckInRepo;

        public WellnessCheckInService(IWellnessCheckInRepo wellnessCheckInRepo)
        {
            _wellnessCheckInRepo = wellnessCheckInRepo;
            
        }

        public async Task<WellnessCheckInDetailed> AddCheckInToDayCardAsync(int dayCardId, WellnessCheckInInputModel checkInInputModel)
        {
            WellnessCheckIn wellnessCheckIn = new(dayCardId, checkInInputModel);

            wellnessCheckIn = await _wellnessCheckInRepo.CreateAsync(wellnessCheckIn);

            return new WellnessCheckInDetailed(wellnessCheckIn);
        }

        public async Task<bool> DeleteCheckInAsync(int checkInId)
        {
            return await _wellnessCheckInRepo.DeleteAsync(checkInId);
        }

        public async Task<WellnessCheckInDetailed> UpdateCheckInAsync(int checkInId, WellnessCheckInInputModel updateInputModel)
        {
            WellnessCheckIn wellnessCheckIn = new(updateInputModel.DayCardId, updateInputModel);

            wellnessCheckIn.Id = checkInId;

            wellnessCheckIn = await _wellnessCheckInRepo.UpdateCheckInAsync(wellnessCheckIn);

            return new WellnessCheckInDetailed(wellnessCheckIn);
        }
    }
}
