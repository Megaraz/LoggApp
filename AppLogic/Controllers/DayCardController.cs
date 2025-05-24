using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;
using AppLogic.Services.Interfaces;
using AppLogic.Controllers.Interfaces;

namespace AppLogic.Controllers
{
    public class DayCardController : IDayCardController
    {

        private readonly IDayCardService _dayCardService;   

        public DayCardController(IDayCardService dayCardService)
        {
            _dayCardService = dayCardService;
        }

        public async Task<DTO_SpecificDayCard> CreateNewDayCardAsync(int userId, DayCardInputModel input)
        {
            return await _dayCardService.CreateNewDayCardAsync(userId, input);

        }

        public async Task<List<DTO_AllDayCard>?> ReadAllDayCardsAsync(int userId)
        {
            return await _dayCardService.ReadAllDayCardsAsync(userId);
        }

        public async Task<DTO_SpecificDayCard?> ReadDayCardSingleAsync(int id, int userId)
        {
            return await _dayCardService.ReadSingleDayCardAsync(id, userId)!;
        }
        public async Task<DTO_SpecificDayCard?> ReadDayCardSingleAsync(DateOnly date, int userId)
        {
            return await _dayCardService.ReadSingleDayCardAsync(date, userId)!;
        }
    }
}
