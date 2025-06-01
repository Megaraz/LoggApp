using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Models;
using AppLogic.Models.DTOs.Detailed;
using AppLogic.Models.DTOs.Summary;
using AppLogic.Models.InputModels;
using Microsoft.IdentityModel.Tokens;
using Presentation.Input;
using Presentation.MenuState_Enums;

namespace Presentation.MenuHandlers
{
    /// <summary>
    /// Handles user-specific menu states in the console application, including user overview, day card management, and user settings.
    /// </summary>
    public class UserMenuHandler : MenuHandlerBase
    {
        private readonly DayCardController _dayCardController;
        private readonly UserController _userController;
        private readonly WeatherController _weatherController;

        public UserMenuHandler(DayCardController dayCardController, UserController userController, WeatherController weatherController)
        {
            _weatherController = weatherController;
            _dayCardController = dayCardController;
            _userController = userController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {
            switch (sessionContext.UserMenuState)
            {
                case UserMenuState.Overview:
                    sessionContext = UserOverviewMenuHandler(sessionContext);
                    break;

                case UserMenuState.AllDayCards:
                    sessionContext = await AllDayCardsMenuHandler(sessionContext);
                    break;

                case UserMenuState.CreateNewDayCard:
                    sessionContext = await CreateDayCardMenuHandlerAsync(sessionContext);
                    break;
                case UserMenuState.SearchDayCard:
                    sessionContext = await SearchDayCardAsync(sessionContext);
                    break;

                case UserMenuState.UserSettings:
                    sessionContext = await UserSettingsMenuHandler(sessionContext);
                    break;

            }

            return sessionContext;

        }

        private async Task<TContext> SearchDayCardAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            string? searchDate = ConsoleInput.GetValidUserInput(MenuText.Prompt.SearchDayCard);

            if (searchDate != null)
            {

                if (!DateOnly.TryParse(searchDate, out DateOnly parsedDate))
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.InvalidDayCardInput);
                    Thread.Sleep(1500);
                    sessionContext.UserMenuState = UserMenuState.Overview;
                    return sessionContext;
                }
                DayCardDetailed? dayCardResult = await _dayCardController.ReadDayCardSingleAsync(parsedDate, sessionContext.CurrentUser!.Id);
                if (dayCardResult == null)
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.NoDayCardFound);
                    Thread.Sleep(1500);
                    sessionContext.UserMenuState = UserMenuState.Overview;
                }
                else
                {
                    sessionContext.CurrentDayCard = dayCardResult;
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                }
            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.Overview;
            }
            return sessionContext;

        }

        public TContext UserOverviewMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}\n";

            var specificUserMenu = MenuText.NavOption.s_SpecificUserMenu.ToList();

            if (sessionContext.CurrentUser.AllDayCardsSummary!.Count > 0)
            {
                //sessionContext.SubHeader = "Number of Daycards: " + sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count + "\n";
                specificUserMenu = specificUserMenu.Prepend(MenuText.NavOption.ShowAllDayCards).ToList();
            }

            specificUserMenu.Add(MenuText.NavOption.Back);

            var specUserChoice = MenuNavigation.GetMenuValue(specificUserMenu, sessionContext);

            if (specUserChoice != null)
            {
                switch (specUserChoice)
                {
                    case MenuText.NavOption.CreateNewDayCard:
                        sessionContext.UserMenuState = UserMenuState.CreateNewDayCard;
                        break;

                    case MenuText.NavOption.ShowAllDayCards:
                        sessionContext.UserMenuState = UserMenuState.AllDayCards;
                        break;

                    case MenuText.NavOption.SearchDayCard:
                        sessionContext.UserMenuState = UserMenuState.SearchDayCard;
                        break;

                    case MenuText.NavOption.UserSettings:
                        sessionContext.UserMenuState = UserMenuState.UserSettings;
                        break;

                    case MenuText.NavOption.Back:
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

        public async Task<TContext> UserSettingsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Get the selected menu option from the user
            var settingChoice = MenuNavigation.GetMenuValue(MenuText.NavOption.s_UserSettingsMenu.ToList(), sessionContext);

            // If the user selected a valid option(not Key.Escape), proceed.
            if (settingChoice is not null)
            {
                switch (settingChoice)
                {
                    case MenuText.NavOption.UpdateUsername:
                        sessionContext = await UpdateUserNameAsync(sessionContext);
                        break;

                    case MenuText.NavOption.UpdateLocation:
                        sessionContext = await UpdateUserLocationAsync(sessionContext);
                        break;

                    case MenuText.NavOption.DeleteUser:
                        sessionContext = await DeleteUserAsync(sessionContext);
                        break;
                }
            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.Overview;
            }

            return sessionContext;
        }

        private async Task<TContext> DeleteUserAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            // Get user input for confirmation
            bool confirmDelete = ConsoleInput.Input_Confirmation(MenuText.Prompt.DeleteUserConfirmation);

            bool userDeleted = false;

            if (confirmDelete)
            {
                userDeleted = await _userController.DeleteUserAsync(sessionContext.CurrentUser!.Id);

                if (userDeleted)
                {

                    // CLIENT SIDE
                    var userSummary = sessionContext.AllUsersSummary?.FirstOrDefault(u => u.Id == sessionContext.CurrentUser!.Id);

                    // Remove the deleted user from the AllUsersSummary list, client-side
                    if (userSummary != null)
                    {
                        sessionContext.AllUsersSummary?.Remove(userSummary);
                    }

                    sessionContext.CurrentUser = null;

                    Console.Clear();
                    Console.WriteLine(MenuText.Header.UserDeleted);
                    Thread.Sleep(1500);
                    sessionContext.MainMenuState = MainMenuState.Main;


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(MenuText.Error.UserDeleteFailed);
                    Thread.Sleep(1500);
                    sessionContext.UserMenuState = UserMenuState.UserSettings;
                }

            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.UserSettings;
            }

            return sessionContext;

        }

        private async Task<TContext> UpdateUserNameAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get user input for username
            string? usernameInput = ConsoleInput.Input_Username(MenuText.Header.CurrentUserName + sessionContext.CurrentUser!.Username, true);

            if (usernameInput is not null)
            {
                // Create a new inputModel with user's existing location
                UserInputModel userInputModel = new UserInputModel(usernameInput)
                {
                    GeoResult = new GeoResult()
                    {
                        Name = sessionContext.CurrentUser!.CityName,
                        Lat = sessionContext.CurrentUser.Lat,
                        Lon = sessionContext.CurrentUser.Lon
                    },

                };

                sessionContext.CurrentUser = await _userController.UpdateUserAsync(sessionContext.CurrentUser!.Id, userInputModel);


                // UPDATE CLIENT-SIDE
                var userSummary = sessionContext.AllUsersSummary?.FirstOrDefault(u => u.Id == sessionContext.CurrentUser!.Id);

                // Update Username on client-side as well
                if (userSummary != null)
                {
                    userSummary.Username = sessionContext.CurrentUser.Username!;
                }


                Console.WriteLine();
                Console.WriteLine("USERNAME UPDATED");
                Thread.Sleep(1500);
                sessionContext.UserMenuState = UserMenuState.Overview;
            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.UserSettings;
            }
            return sessionContext;
        }

        private async Task<TContext> UpdateUserLocationAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get user input for geocode/location search
            string? locationInput = ConsoleInput.Input_Location(MenuText.Header.CurrentLocation + sessionContext.CurrentUser!.CityName, true);


            if (locationInput is not null)
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _weatherController.LocationGeoResultList(locationInput!);

                // Create a new inputModel with users existing name
                UserInputModel userInputModel = new UserInputModel(sessionContext.CurrentUser!.Username!);


                // User chooses a Location from the list of locations
                userInputModel.GeoResult = MenuNavigation.GetMenuValue(geoResultResponse.Results, sessionContext)!;

                if (userInputModel.GeoResult is not null)
                {
                    sessionContext.CurrentUser = await _userController.UpdateUserAsync(sessionContext.CurrentUser!.Id, userInputModel);

                    // UPDATE CLIENT-SIDE
                    var userSummary = sessionContext.AllUsersSummary?.FirstOrDefault(u => u.Id == sessionContext.CurrentUser!.Id);

                    // Update Cityname on client-side
                    if (userSummary != null)
                    {
                        userSummary.CityName = sessionContext.CurrentUser.CityName;
                    }

                    Console.WriteLine();
                    Console.WriteLine("LOCATION UPDATED");
                    Thread.Sleep(1500);
                    sessionContext.UserMenuState = UserMenuState.UserSettings;
                }
            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.UserSettings;
            }
            return sessionContext;
        }

        public async Task<TContext> AllDayCardsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            if (sessionContext.CurrentUser!.AllDayCardsSummary == null || sessionContext.CurrentUser.AllDayCardsSummary.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoDayCardsFound;
                sessionContext.UserMenuState = UserMenuState.Overview;
            }
            else
            {
                sessionContext.MainHeader = sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.CurrentUser!.ToString()}\n";
                sessionContext.SubHeader = MenuText.Header.AllDayCards;

                var dayCardChoice = MenuNavigation.GetMenuValue(sessionContext!.CurrentUser!.AllDayCardsSummary!, sessionContext);

                if (dayCardChoice != null)
                {
                    sessionContext.CurrentDayCard = await _dayCardController.ReadDayCardSingleAsync(dayCardChoice.DayCardId, sessionContext.CurrentUser.Id);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                }
                else
                {
                    sessionContext.UserMenuState = UserMenuState.Overview;
                }


            }

            return sessionContext;
        }

        public async Task<TContext> CreateDayCardMenuHandlerAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            //sessionContext.MainHeader = MenuText.Prompt.CreateDayCard;
            DayCardInputModel? dayCardInputModel = ConsoleInput.Input_DayCard(sessionContext);

            if (dayCardInputModel != null)
            {
                sessionContext.CurrentDayCard = await _dayCardController.CreateNewDayCardAsync(sessionContext.CurrentUser!.Id, dayCardInputModel);

                // BELOW CODE IS FOR CLIENT-SIDE UPDATING ALL INSTANCES WHERE NEW DAYCARD SHOULD BE

                sessionContext.CurrentUser.AllDayCardsSummary ??= new List<DayCardSummary>();

                sessionContext.CurrentUser.AllDayCardsSummary
                    .Add(new DayCardSummary(sessionContext.CurrentDayCard));

                sessionContext.CurrentUser.DayCardCount = sessionContext.CurrentUser.AllDayCardsSummary.Count; // Update the DayCardCount of the CurrentUser

                sessionContext.AllUsersSummary! // Update the User within the All Users list, with the new DayCard count
                    .FirstOrDefault(u => u.Id == sessionContext.CurrentUser!.Id)!.DayCardCount = (int)sessionContext.CurrentUser.DayCardCount;

                sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.Overview;
            }

            return sessionContext;
        }

    }
}
