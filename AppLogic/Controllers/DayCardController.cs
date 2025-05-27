using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Services.Interfaces;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.DTOs.Detailed;

namespace AppLogic.Controllers
{
    public class DayCardController
    {

        private readonly IDayCardService _dayCardService;   

        public DayCardController(IDayCardService dayCardService)
        {
            _dayCardService = dayCardService;
        }

        public async Task<DayCardDetailed> CreateNewDayCardAsync(int userId, DayCardInputModel input)
        {
            return await _dayCardService.CreateNewDayCardAsync(userId, input);

        }

        public async Task<bool> DeleteDayCardAsync(int dayCardId)
        {
            return await _dayCardService.DeleteDayCardAsync(dayCardId);
        }

        public async Task<List<DayCardSummary>?> ReadAllDayCardsAsync(int userId)
        {
            return await _dayCardService.ReadAllDayCardsAsync(userId);
        }

        public async Task<DayCardDetailed?> ReadDayCardSingleAsync(int id, int userId)
        {
            return await _dayCardService.ReadSingleDayCardAsync(id, userId)!;
        }
        public async Task<DayCardDetailed?> ReadDayCardSingleAsync(DateOnly date, int userId)
        {
            return await _dayCardService.ReadSingleDayCardAsync(date, userId)!;
        }
    }
}
