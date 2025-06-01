using System.Text.Json;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.Entities;
using AppLogic.Models.InputModels;
using AppLogic.Repositories;
using AppLogic.Repositories.Interfaces;
using AppLogic.Services.Interfaces;

namespace AppLogic.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepo _userRepository;

        public UserService(IUserRepo userRepo)
        {
            _userRepository = userRepo;
        }
        


        public async Task<UserDetailed> RegisterNewUserAsync(UserInputModel inputModel)
        {

            User newUser = new User(inputModel);

            newUser = await _userRepository.CreateAsync(newUser);

            return new UserDetailed()
            {
                Id = newUser.Id,
                Username = newUser.Username!,
                CityName = newUser.CityName,
                Lat = newUser.Lat,
                Lon = newUser.Lon,
                AllDayCardsSummary = newUser.DayCards!
                    .Select(dayCard => new DayCardSummary(dayCard)).ToList(),
                DayCardCount = newUser.DayCards?.Count ?? 0
            };

        }



        public async Task<List<UserSummary>?> GetAllUsersIncludeAsync()
        {
            List<User>? users = await _userRepository.GetAllUsersIncludeAsync();

            if (users == null)
            {
                return null;
            }

            List<UserSummary> DTO_AllUsers = new List<UserSummary>();

            foreach (var user in users)
            {
                DTO_AllUsers.Add
                    (
                        new UserSummary()
                        {
                            Id = user.Id,
                            Username = user.Username!,
                            CityName = user.CityName,
                            DayCardCount = user.DayCards?.Count ?? 0,

                        }
                    );
            }

            return DTO_AllUsers.OrderBy(x => x.Username).ToList();
        }

        public async Task<UserDetailed?> GetUserByIdIncludeAsync(int id)
        {
            var user = await _userRepository.GetUserByIdIncludeAsync(id);

            if (user == null) return null;


            return new UserDetailed()
            {
                Id = user!.Id,
                Username = user.Username!,
                CityName = user.CityName,
                Lat = user.Lat,
                Lon = user.Lon,
                AllDayCardsSummary = user.DayCards!
                    .Select(dayCard => new DayCardSummary(dayCard)).ToList(),
                DayCardCount = user.DayCards?.Count ?? 0
            };
        }
        public async Task<UserDetailed?> GetUserByUsernameIncludeAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameIncludeAsync(username);

            if ( user == null ) return null;

            return new UserDetailed()
            {
                Id = user.Id,
                Username = user.Username!,
                CityName = user.CityName,
                Lat = user.Lat,
                Lon = user.Lon,
                AllDayCardsSummary = user.DayCards!
               .Select(dayCard => new DayCardSummary(dayCard)).ToList(),
                DayCardCount = user.DayCards?.Count ?? 0
            };
        }

        public async Task<UserDetailed> UpdateUserAsync(int userId, UserInputModel inputModel)
        {
            User user = new User(inputModel)
            {
                Id = userId,
            };
            user = await _userRepository.UpdateUserIncludeAsync(user);

            return new UserDetailed()
            {
                Id = user.Id,
                Username = user.Username!,
                CityName = user.CityName,
                Lat = user.Lat,
                Lon = user.Lon,
                AllDayCardsSummary = user.DayCards!
                    .Select(dayCard => new DayCardSummary(dayCard)).ToList(),
                DayCardCount = user.DayCards?.Count ?? 0
            };
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }
    }
}
