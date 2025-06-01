using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models;
using AppLogic.Services;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Repositories;
using AppLogic.Models.InputModels;
using Presentation.Input;

namespace Presentation.MenuHandlers
{
    /// <summary>
    /// Handles the main menu state of the console application, allowing users to navigate through various options such as logging in, viewing all users, creating a new user, and checking today's weather.
    /// </summary>
    public class MainMenuHandler : MenuHandlerBase
    {

        private readonly UserController _userController;
        private readonly WeatherController _weatherController;

        public MainMenuHandler(UserController userController, WeatherController weatherController)
        {
            _userController = userController;
            _weatherController = weatherController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {

            switch (sessionContext.MainMenuState)
            {
                case MainMenuState.Main:
                    await InitMenuHandler(sessionContext);
                    break;

                case MainMenuState.AllUsers:
                    await AllUsersMenuHandler(sessionContext);
                    break;

                case MainMenuState.TodaysWeather:
                    await GetTodaysWeather(sessionContext);
                    break;

                case MainMenuState.Exit:
                    break;


            }
            return sessionContext;
        }

        public async Task<TContext> InitMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.InitMenu;

            sessionContext.AllUsersSummary = await _userController.GetAllUsersIncludeAsync()!;


            var initMenuChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_InitMenu.ToList(), sessionContext);
            //Console.WriteLine($"THERE ARE CURRENTLY {sessionContext.UserCountInDb} USERS IN DB");

            if (initMenuChoice != null)
            {
                switch (initMenuChoice)
                {
                    case MenuText.NavOption.Login:
                        await Login(sessionContext);
                        break;

                    case MenuText.NavOption.GetAllUsers:
                        await GetAllUsers(sessionContext);
                        break;

                    case MenuText.NavOption.CreateNewUser:
                        await CreateNewUser(sessionContext);
                        break;

                    case MenuText.NavOption.GetTodaysWeather:
                        await GetTodaysWeather(sessionContext);
                        break;

                    case MenuText.NavOption.Exit:
                        sessionContext.MainMenuState = MainMenuState.Exit;
                        break;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Exit;
            }

            return sessionContext;
        }

        public async Task<TContext> AllUsersMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            if (sessionContext.AllUsersSummary == null || sessionContext.AllUsersSummary.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;

            }
            else
            {
                sessionContext.MainHeader = MenuText.Header.AllUsers;
                //sessionContext.MainHeader = MenuText.Header.SpecificUser;

                var userChoice = MenuNavigation.GetMenuValue(sessionContext.AllUsersSummary, sessionContext);

                if (userChoice != null)
                {
                    sessionContext
                        .CurrentUser = await _userController.GetUserDetailed(userChoice.Id);

                    sessionContext
                        .UserMenuState = UserMenuState.Overview;
                }
                else
                {
                    sessionContext.MainMenuState = MainMenuState.Main;
                }

            }
            return sessionContext;
        }

        private async Task<TContext> CreateNewUser<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Get basic user info/input
            UserInputModel userInputModel = ConsoleInput.Input_User()!;

            if (userInputModel != null)
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _weatherController.UserGeoResultList(userInputModel);

                // User chooses a Location from the list of locations
                sessionContext.CurrentPrompt = MenuText.Prompt.ChooseLocation;
                userInputModel.GeoResult = MenuNavigation.GetMenuValue(geoResultResponse.Results, sessionContext)!;


                if (userInputModel.GeoResult != null)
                {
                    // Create a new user with the updated GeoResult, and sets the CurrentUser to the newly created one
                    sessionContext.CurrentUser = await _userController.CreateNewUserAsync(userInputModel);

                    sessionContext.AllUsersSummary ??= new List<UserSummary>();

                    sessionContext.AllUsersSummary.Add(new UserSummary(sessionContext.CurrentUser));    

                    sessionContext.UserMenuState = UserMenuState.Overview;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Main;
            }
            return sessionContext;
        }

        private async Task<TContext> GetAllUsers<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            List<UserSummary>? allUsers = await _userController.GetAllUsersIncludeAsync()!;

            if (allUsers == null || allUsers.Count == 0)
            {
                Console.Clear();
                Console.WriteLine(MenuText.Error.NoUsersFound);
                Thread.Sleep(1500);
                sessionContext.MainMenuState = MainMenuState.Main;
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.AllUsers;
                sessionContext.AllUsersSummary = allUsers;
            }

            return sessionContext;
        }

        private async Task<TContext> Login<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            string? username = ConsoleInput.GetValidUserInput(null, MenuText.Prompt.EnterUserName, MenuText.Error.InvalidUserNameInput);

            if (username != null)
            {

                //UserDetailed? userDetailed = await _userController.ReadUserSingleAsync(username);
                UserDetailed? userDetailed = await _userController.GetUserDetailed(username);

                if (userDetailed == null)
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.NoUserFound);
                    Thread.Sleep(1500);
                    sessionContext.MainMenuState = MainMenuState.Main;

                }
                else
                {
                    sessionContext.CurrentUser = userDetailed;
                    sessionContext.UserMenuState = UserMenuState.Overview;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Main;
            }

            return sessionContext;
        }

        /// <summary>
        /// Fetches today's weather for a given location input by the user, generates a summary using AI, and displays it.
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="sessionContext"></param>
        /// <returns></returns>
        private async Task<TContext> GetTodaysWeather<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get basic user info/input
            string? location = ConsoleInput.Input_Location();


            if (!location.IsNullOrEmpty())
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _weatherController.LocationGeoResultList(location!);
                // User chooses a Location from the list of locations
                GeoResult? geoResult = MenuNavigation.GetMenuValue(geoResultResponse.Results, sessionContext);

                if (geoResult != null)
                {
                    string lat = geoResult.Lat?.ToString(CultureInfo.InvariantCulture)!;
                    string lon = geoResult.Lon?.ToString(CultureInfo.InvariantCulture)!;
                    string date = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;
                    var weatherData = await _weatherController.GetWeatherDataAsync(lat, lon, date);
                    var prompt = AiPromptBuilder.BuildWeatherPrompt(weatherData);

                    var ai = new OpenAiResponseClient();

                    weatherData.AISummary = await ai.GenerateSummaryAsync(prompt);

                    WeatherDataSummary todayWeather = _weatherController.ConvertToDTO(weatherData);

                    Console.WriteLine(todayWeather.ToString());

                    Console.ReadLine();

                    sessionContext.MainMenuState = MainMenuState.Main;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Main;
            }

            return sessionContext;
        }

        
}
}


