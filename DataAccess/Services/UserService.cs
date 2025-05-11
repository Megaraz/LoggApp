using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.DTOs;
using BusinessLogic.Models;
using DataAccess.Repositories;
using Presentation;

namespace AppLogic.Services
{
    public static class UserService
    {

        public static async Task RegisterNewUserAsync(UserInputModel input)
        {
            Task<string> TaskResultString = new WeatherRepo(input.CityName).GetGeoCodeAsync();

            User newUser = new User()
            {
                Username = input.Username
            };
            var locations = JsonSerializer.Deserialize<List<GeoResult>>(await TaskResultString);


            if (locations != null && locations.Count > 0)
            {
                newUser.CityName = locations[0].Name;
                newUser.Lat = locations[0].Lat;
                newUser.Lon = locations[0].Lon;
            }

            UserRepository.Create(newUser);
        }

        public static async Task<List<AllUserMenuDto>> ReadAllUsersAsync()
        {
            var usersTask = await UserRepository.ReadAllAsync();
            return usersTask.OrderBy(x => x.Id).ToList();



            //Dictionary<int, string> allUsersView = new Dictionary<int, string>();

            //foreach (var user in await usersTask)
            //{
            //    allUsersView.Add(user.Id, $"{user.Username}, {user.CityName}, Daycards: {user.DayCardCount}");
            //}


            //List<string> users = new List<string>();

            //foreach (var user in await usersTask)
            //{
            //    users.Add($"[{user.Id}]. {user.Username}, {user.CityName}, Daycards: {user.DayCardCount}");
            //}

            //return users.OrderBy(x => x).ToList();

            //return await UserRepository.ReadAllAsync();
        }

        public static async Task<User>? ReadSingleUserAsync(int id)
        {
            var userTask = await UserRepository.ReadSingle(id);
            return userTask!;
        }
        public static async Task<User>? ReadSingleUserAsync(string username)
        {
            var userTask = await UserRepository.ReadSingle(username);
            return userTask!;
        }
    }
}
