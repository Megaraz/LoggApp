using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppLogic.DTOs;
using AppLogic.Models;
using BusinessLogic.Models;
using DataAccess.Repositories;
using Presentation;

namespace AppLogic.Services
{
    public static class UserService
    {


        public static async Task<GeoResultsResponse> GetLocationsMenu(string city)
        {
            Task<string> TaskResultString = new WeatherRepo().GetGeoCodeAsync(city);

            return JsonSerializer.Deserialize<GeoResultsResponse>(await TaskResultString)!;
        }
           
        public static async Task<SpecificUserMenuDto> RegisterNewUserAsync(UserInputModel input)
        {


            User newUser = new User(input);



            //return await UserRepository.CreateAsync(newUser);


        }



        public static async Task<List<AllUserMenuDto>> ReadAllUsersAsync()
        {
            var usersTask = await UserRepository.ReadAllAsync();
            return usersTask.OrderBy(x => x.Id).ToList();

        }

        public static async Task<SpecificUserMenuDto>? ReadSingleUserAsync(int id)
        {
            var userTask = await UserRepository.ReadSingleAsync(id);
            return userTask!;
        }
        public static async Task<SpecificUserMenuDto>? ReadSingleUserAsync(string username)
        {
            var userTask = await UserRepository.ReadSingleAsync(username);
            return userTask!;
        }
    }
}
