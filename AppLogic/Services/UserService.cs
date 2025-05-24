using System.Text.Json;
using AppLogic.Models;
using AppLogic.Models.DTOs;
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
        


        public async Task<DTO_SpecificUser> RegisterNewUserAsync(UserInputModel input)
        {

            User newUser = new User(input);

            newUser = await _userRepository.CreateAsync(newUser);

            return new DTO_SpecificUser()
            {
                Id = newUser.Id,
                Username = newUser.Username!,
                CityName = newUser.CityName,
                Lat = newUser.Lat,
                Lon = newUser.Lon,
                DTO_AllDayCards = newUser.DayCards!
                    .Select(d => new DTO_AllDayCard
                    {
                        DayCardId = d.Id,
                        UserId = d.UserId,
                        Date = d.Date,
                        Entries = (d.Activities!.Count + d.CaffeineDrinks!.Count + d.Supplements!.Count)

                    }).ToList()
            };

        }



        public async Task<List<DTO_AllUser>?> ReadAllUsersAsync()
        {
            List<User>? users = await _userRepository.GetAllUsersIncludeAsync();

            if (users == null)
            {
                return null;
            }

            List<DTO_AllUser> DTO_AllUsers = new List<DTO_AllUser>();

            foreach (var user in users)
            {
                DTO_AllUsers.Add
                    (
                        new DTO_AllUser()
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

        public async Task<DTO_SpecificUser?> ReadSingleUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdIncludeAsync(id);

            if (user == null) return null;


            return new DTO_SpecificUser()
            {
                Id = user!.Id,
                Username = user.Username!,
                CityName = user.CityName,
                Lat = user.Lat,
                Lon = user.Lon,
                DTO_AllDayCards = user.DayCards!
                    .Select(d => new DTO_AllDayCard
                    {
                        DayCardId = d.Id,
                        UserId = d.UserId,
                        Date = d.Date,
                        Entries = (d.Activities!.Count + d.CaffeineDrinks!.Count + d.Supplements!.Count)

                    }).ToList()
            };
        }
        public async Task<DTO_SpecificUser?> ReadSingleUserAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameIncludeAsync(username);

            if ( user == null ) return null;

            return new DTO_SpecificUser()
            {
                Id = user.Id,
                Username = user.Username!,
                CityName = user.CityName,
                Lat = user.Lat,
                Lon = user.Lon,
                DTO_AllDayCards = user.DayCards!
               .Select(d => new DTO_AllDayCard
               {
                   DayCardId = d.Id,
                   UserId = d.UserId,
                   Date = d.Date,
                   Entries = (d.Activities!.Count + d.CaffeineDrinks!.Count + d.Supplements!.Count)

               }).ToList()
            };
        }

    }
}
