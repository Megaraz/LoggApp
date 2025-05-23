using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models.DTOs;
using AppLogic.Models;
using AppLogic.Services;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class MainMenuHandler : MenuHandlerBase
    {
        public MainMenuHandler(Controller controller) : base(controller)
        {
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

        public async Task<TContext> AllUsersMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            if (sessionContext.DTO_AllUsers == null || sessionContext.DTO_AllUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;

            }
            else
            {
                sessionContext.MainHeader = MenuText.Header.AllUsers;

                var userChoice = GetMenuValue(sessionContext.DTO_AllUsers, sessionContext);

                if (userChoice != null)
                {
                    sessionContext
                        .DTO_CurrentUser = await _controller.ReadUserSingleAsync(userChoice.Id);

                    sessionContext
                        .MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}\n\n\n";

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

        private async Task<TContext> CreateNewUser<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Get basic user info/input
            UserInputModel userInputModel = View.Input_User()!;

            if (userInputModel != null)
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _controller.UserGeoResultList(userInputModel);

                // User chooses a Location from the list of locations
                sessionContext.CurrentPrompt = MenuText.Prompt.CreateUserLocationChoice;
                userInputModel.GeoResult = GetMenuValue(geoResultResponse.Results, sessionContext);


                if (userInputModel.GeoResult != null)
                {
                    // Create a new user with the updated GeoResult, and sets the CurrentUser to the newly created one
                    sessionContext.DTO_CurrentUser = await _controller.CreateNewUserAsync(userInputModel);
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
            List<DTO_AllUser>? allUsers = await _controller.ReadAllUsersAsync()!;

            if (allUsers == null || allUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.MainMenuState = MainMenuState.Main;
            }
            else
            {
                sessionContext.MainMenuState = MainMenuState.AllUsers;
                //sessionContext.CurrentMainMenu = MenuText.NavOption.s_AllUserMenu.ToList();
                sessionContext.DTO_AllUsers = allUsers;

            }

            return sessionContext;
        }
        private async Task<TContext> Login<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            string? username = View.GetValidUserInput(MenuText.Prompt.CreateUser, MenuText.Error.InvalidUserNameInput);

            if (username != null)
            {

                DTO_SpecificUser? resultUser = await _controller.ReadUserSingleAsync(username);

                if (resultUser == null)
                {
                    sessionContext.ErrorMessage = MenuText.Error.NoUserFound;
                    sessionContext.MainMenuState = MainMenuState.Main;

                }
                else
                {
                    sessionContext.DTO_CurrentUser = resultUser;
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
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";

            var specificUserMenu = MenuText.NavOption.s_SpecificUserMenu.ToList();

            if (sessionContext.DTO_CurrentUser.DTO_AllDayCards!.Count > 0)
            {
                sessionContext.SubHeader = "Number of Daycards: " + sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count;
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
                // Get list of locations that match users input location%
                GeoResultResponse geoResultResponse = await _controller.LocationGeoResultList(location!);
                // User chooses a Location from the list of locations
                GeoResult geoResult = GetMenuValue(geoResultResponse.Results, sessionContext);

                if (geoResult != null)
                {
                    string lat = geoResult.Lat?.ToString(CultureInfo.InvariantCulture)!;
                    string lon = geoResult.Lon?.ToString(CultureInfo.InvariantCulture)!;
                    string date = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)!;
                    DTO_AllWeatherData todayWeather = WeatherService.ConvertToDTO(await WeatherService.GetWeatherDataAsync(lat, lon, date));

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


