using System.Text.Json;
using AppLogic.Models;
using AppLogic.Models.DTOs;
using DataAccess;
using DataAccess.Repositories;

namespace AppLogic.Services
{
    public class UserService
    {

        private readonly LoggAppContext _dbContext;
        private readonly UserRepo _userRepository;

        public UserService(LoggAppContext dbContext)
        {
            _dbContext = dbContext;
            _userRepository = new UserRepo(dbContext);
        }
        public async Task<GeoResultResponse> GetGeoResultAsync(string city)
        {
            Task<string> TaskResultString = WeatherRepo.GetGeoCodeAsync(city);

            return JsonSerializer.Deserialize<GeoResultResponse>(await TaskResultString)!;
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




        //public async Task<DTO_SpecificUser> RegisterNewUserAsync(UserInputModel input)
        //{

        //    User newUser = new User(input);

        //    newUser = await _userRepository.CreateAsync(newUser);

        //    return new DTO_SpecificUser()
        //{
        //    Id = newUser.Id,
        //        Username = newUser.Username!,
        //        CityName = newUser.CityName,
        //        Lat = newUser.Lat,
        //        Lon = newUser.Lon,
        //        AllDayCardsMenu = newUser.DayCards!
        //            .Select(d => new DTO_AllDayCards
        //            {
        //                DayCardId = d.Id,
        //                UserId = d.UserId,
        //                Date = d.Date

        //            }).ToList()
        //    };

        //}



        //public async Task<List<DTO_AllUser>> ReadAllUsersAsync()
        //{
        //    var usersTask = await _userRepository.ReadAllAsync();
        //    return usersTask.OrderBy(x => x.Id).ToList();

        //}

        //public async Task<DTO_SpecificUser>? ReadSingleUserAsync(int id)
        //{
        //    var userTask = await _userRepository.ReadSingleAsync(id);
        //    return userTask!;
        //}
        //public async Task<DTO_SpecificUser>? ReadSingleUserAsync(string username)
        //{
        //    var userTask = await _userRepository.ReadSingleAsync(username);
        //    return userTask!;
        //}
    }
}
