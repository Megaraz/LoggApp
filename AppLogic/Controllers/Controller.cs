using AppLogic.Models;
using AppLogic.Models.DTOs;
using AppLogic.Models.Intake.InputModels;
using AppLogic.Services;

namespace AppLogic.Controllers
{
    public class Controller
    {
        private readonly LoggAppContext _dbContext;
        private readonly UserService _userService;
        private readonly DayCardService _dayCardService;
        private readonly CaffeineDrinkService _caffeineDrinkService;
        public Controller(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            _userService = new UserService(dbContext);
            _dayCardService = new DayCardService(dbContext);
            _caffeineDrinkService = new CaffeineDrinkService(dbContext);
        }

        public async Task<DTO_SpecificDayCard> CreateNewDayCardAsync(int userId, DayCardInputModel input)
        {
            return await _dayCardService.CreateNewDayCardAsync(userId, input);

        }

        public async Task<DTO_SpecificCaffeineDrink> AddCaffeineDrinkToDayCardAsync(int dayCardId, CaffeineDrinkInputModel input)
        {
            return await _caffeineDrinkService.AddCaffeineDrinkToDayCardAsync(dayCardId, input);
        }



        public async Task<GeoResultResponse> LocationGeoResultList(string cityName)
        {
            return await _userService.GetGeoResultAsync(cityName);

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
