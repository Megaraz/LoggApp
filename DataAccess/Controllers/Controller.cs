using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Services;
using BusinessLogic.Models;
using DataAccess;
using Presentation;

namespace AppLogic.Controllers
{
    public class Controller
    {
        private readonly LoggAppContext _dbContext;
        private readonly UserService _userService;
        private readonly DayCardService _dayCardService;
        public Controller(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            _dayCardService = new DayCardService(dbContext);
            _userService = new UserService(dbContext);
        }

        public async Task<DTO_SpecificDayCard> CreateNewDayCardAsync(DayCardInputModel input)
        {
            return await _dayCardService.CreateNewDayCardAsync(input);

        }

        public async Task<GeoResultResponse> UserGeoResultList(UserInputModel input)
        {
            return await _userService.GetGeoResultAsync(input.CityName);

        }

        public async Task<DTO_SpecificUser> CreateNewUserAsync(UserInputModel input)
        {
            return await _userService.RegisterNewUserAsync(input);
        }

        public async Task<List<DTO_AllUser>?> ReadAllUsersAsync()
        {

            var users = await _userService.ReadAllUsersAsync();


            if (users != null && users.Count > 0)
            {
                return users;
            }
            else
            {
                return null;
            }


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



        public async Task<DTO_SpecificUser?> ReadUserSingleAsync(int id)
        {
            return await _userService.ReadSingleUserAsync(id)!;

        }

        public async Task<DTO_SpecificUser?> ReadUserSingleAsync(string username)
        {

            return await _userService.ReadSingleUserAsync(username)!;

        }

    }
}
