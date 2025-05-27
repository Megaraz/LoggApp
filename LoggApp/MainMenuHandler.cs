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

namespace Presentation
{
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

                case MainMenuState.SpecificUser:
                    SpecificUserMenuHandler(sessionContext);
                    break;

                case MainMenuState.AllUsers:
                    await AllUsersMenuHandler(sessionContext);
                    break;

                case MainMenuState.TodaysWeather:
                    await GetTodaysWeather(sessionContext);
                    break;

                case MainMenuState.Back:
                    goto case MainMenuState.Main;

                case MainMenuState.Exit:
                    break;


            }
            return sessionContext;
        }
        public async Task<TContext> InitMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.InitMenu;

            var initMenuChoice = GetMenuValue(MenuText.NavOption.s_InitMenu.ToList(), sessionContext);

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
            if (sessionContext.CurrentUsersSummary == null || sessionContext.CurrentUsersSummary.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;

            }
            else
            {
                sessionContext.MainHeader = MenuText.Header.AllUsers;
                //sessionContext.MainHeader = MenuText.Header.SpecificUser;

                var userChoice = GetMenuValue(sessionContext.CurrentUsersSummary, sessionContext);

                if (userChoice != null)
                {
                    sessionContext
                        .CurrentUser = await _userController.ReadUserSingleAsync(userChoice.Id);

                    sessionContext
                        .MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}\n\n\n";

                    sessionContext
                        .MainMenuState = MainMenuState.SpecificUser;
                }
                else
                {
                    sessionContext.MainMenuState = MainMenuState.Back;
                }

            }
            return sessionContext;
        }

        

        private async Task<TContext> CreateNewUser<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Get basic user info/input
            UserInputModel userInputModel = View.Input_User()!;

            if (userInputModel != null)
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _weatherController.UserGeoResultList(userInputModel);

                // User chooses a Location from the list of locations
                sessionContext.CurrentPrompt = MenuText.Prompt.ChooseLocation;
                userInputModel.GeoResult = GetMenuValue(geoResultResponse.Results, sessionContext)!;


                if (userInputModel.GeoResult != null)
                {
                    // Create a new user with the updated GeoResult, and sets the CurrentUser to the newly created one
                    sessionContext.CurrentUser = await _userController.CreateNewUserAsync(userInputModel);
                    sessionContext.MainMenuState = MainMenuState.SpecificUser;

                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Back;
            }
            return sessionContext;
        }

        private async Task<TContext> GetAllUsers<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            List<UserSummary>? allUsers = await _userController.GetAllUsersIncludeAsync()!;

            if (allUsers == null || allUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.AllUsers;
                //sessionContext.CurrentMainMenu = MenuText.NavOption.s_AllUserMenu.ToList();
                sessionContext.CurrentUsersSummary = allUsers;

            }

            return sessionContext;
        }
        private async Task<TContext> Login<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            string? username = View.GetValidUserInput(null, MenuText.Prompt.EnterUserName, MenuText.Error.InvalidUserNameInput);

            if (username != null)
            {

                UserDetailed? resultUser = await _userController.ReadUserSingleAsync(username);

                if (resultUser == null)
                {
                    sessionContext.ErrorMessage = MenuText.Error.NoUserFound;
                    Console.Clear();
                    Console.WriteLine(sessionContext.ErrorMessage);
                    Thread.Sleep(1500);
                    sessionContext.MainMenuState = MainMenuState.Main;

                }
                else
                {
                    sessionContext.CurrentUser = resultUser;
                    sessionContext.MainMenuState = MainMenuState.SpecificUser;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Back;
            }

            return sessionContext;
        }

        public TContext SpecificUserMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}\n";

            var specificUserMenu = MenuText.NavOption.s_SpecificUserMenu.ToList();

            if (sessionContext.CurrentUser.DayCardSummary!.Count > 0)
            {
                //sessionContext.SubHeader = "Number of Daycards: " + sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count + "\n";
                specificUserMenu = specificUserMenu.Prepend(MenuText.NavOption.ShowAllDayCards).ToList();
            }

            specificUserMenu.Add(MenuText.NavOption.Back);

            var specUserChoice = GetMenuValue(specificUserMenu, sessionContext);

            if (specUserChoice != null)
            {
                switch (specUserChoice)
                {
                    case MenuText.NavOption.CreateNewDayCard:
                        ResetMenuStates(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.CreateNewDayCard;
                        //sessionContext.MainMenuState = MainMenuState.None;
                        break;

                    case MenuText.NavOption.ShowAllDayCards:
                        ResetMenuStates(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.AllDayCards;
                        break;

                    case MenuText.NavOption.SearchDayCard:
                        ResetMenuStates(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.SearchDayCard;
                        break;

                    case MenuText.NavOption.UserSettings:
                        ResetMenuStates(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.UserSettings;
                        break;

                    case MenuText.NavOption.Back:
                        ResetMenuStates(sessionContext);
                        sessionContext.MainMenuState = MainMenuState.AllUsers;
                        break;
                }

            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.AllUsers;
            }


            return sessionContext;
        }
        private async Task<TContext> GetTodaysWeather<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get basic user info/input
            string? location = View.Input_Location();


            if (!location.IsNullOrEmpty())
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _weatherController.LocationGeoResultList(location!);
                // User chooses a Location from the list of locations
                GeoResult? geoResult = GetMenuValue(geoResultResponse.Results, sessionContext);

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

                    //var apiKey = Environment.GetEnvironmentVariable("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")
                    //    ?? throw new InvalidOperationException("Sätt OPENAI_API_KEY!");

                    

                    Console.WriteLine(todayWeather.ToString());

                    Console.ReadLine();


                    sessionContext.MainMenuState = MainMenuState.Back;
                }
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.Back;
            }

            return sessionContext;
        }

        
}
}


