using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Controllers;
using AppLogic.Controllers.Interfaces;
using AppLogic.Models;
using Microsoft.IdentityModel.Tokens;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class UserMenuHandler : MenuHandlerBase
    {
        private readonly IDayCardController _dayCardController;
        private readonly IUserController _userController;
        private readonly IWeatherController _weatherController;

        public UserMenuHandler(IDayCardController dayCardController, IUserController userController, IWeatherController weatherController)
        {
            _weatherController = weatherController;
            _dayCardController = dayCardController;
            _userController = userController;
        }

        public override async Task<TContext> HandleMenuState<TContext>(TContext sessionContext)
        {
            switch (sessionContext.UserMenuState)
            {
                case UserMenuState.AllDayCards:
                    sessionContext = await AllDayCardsMenuHandler(sessionContext);
                    break;

                case UserMenuState.CreateNewDayCard:
                    sessionContext = await CreateDayCardMenuHandler(sessionContext);
                    break;
                case UserMenuState.SearchDayCard:
                    break;

                case UserMenuState.UserSettings:
                    sessionContext = await UserSettingsMenuHandler(sessionContext);
                    break;

                case UserMenuState.Back:
                    ResetMenuStates(sessionContext);
                    sessionContext.MainMenuState = MainMenuState.SpecificUser;
                    break;
            }

            return sessionContext;

        }

        public async Task<TContext> UserSettingsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);

            // Get the selected menu option from the user
            var settingChoice = GetMenuValue(MenuText.NavOption.s_UserSettingsMenu.ToList(), sessionContext);

            // If the user selected a valid option(not Key.Escape), proceed.
            if (settingChoice is not null)
            {
                switch (settingChoice)
                {
                    case MenuText.NavOption.UpdateUsername:
                        sessionContext = await UpdateUserNameAsync(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.Back;
                        break;

                    case MenuText.NavOption.UpdateLocation:
                        sessionContext = await UpdateUserLocationAsync(sessionContext);
                        sessionContext.UserMenuState = UserMenuState.Back;
                        break;

                    case MenuText.NavOption.DeleteUser:
                        sessionContext = await DeleteUserAsync(sessionContext);
                        
                        break;
                }
            }

            return sessionContext;
        }

        private async Task<TContext> DeleteUserAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            // Get user input for confirmation
            bool confirmDelete = View.Input_Confirmation(MenuText.Prompt.PromptDeleteUserConfirmation);

            bool userDeleted = false;

            if (confirmDelete)
            {
                userDeleted = await _userController.DeleteUserAsync(sessionContext.UserDetailed!.Id);

                if (userDeleted)
                {
                    sessionContext.UserDetailed = null;
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
                }

            }

            return sessionContext;

        }

        private async Task<TContext> UpdateUserNameAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get user input for username
            string? usernameInput = View.Input_Username(MenuText.Header.CurrentUserName + sessionContext.UserDetailed!.Username, true);

            if (usernameInput is not null)
            {
                // Create a new inputModel with users existing location
                UserInputModel userInputModel = new UserInputModel()
                {
                    GeoResult = new GeoResult()
                    {
                        Name = sessionContext.UserDetailed!.CityName,
                        Lat = sessionContext.UserDetailed.Lat,
                        Lon = sessionContext.UserDetailed.Lon
                    },
                    Username = usernameInput
                };

                sessionContext.UserDetailed = await _userController.UpdateUserAsync(sessionContext.UserDetailed!.Id, userInputModel);
            }
            return sessionContext;
        }

        private async Task<TContext> UpdateUserLocationAsync<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get user input for geocode/location search
            string? locationInput = View.Input_Location(MenuText.Header.CurrentLocation + sessionContext.UserDetailed!.CityName, true);


            if (locationInput is not null)
            {
                // Get list of locations that match users input location
                GeoResultResponse geoResultResponse = await _weatherController.LocationGeoResultList(locationInput!);

                // Create a new inputModel with users existing name
                UserInputModel userInputModel = new UserInputModel()
                {
                    Username = sessionContext.UserDetailed!.Username!
                };

                // User chooses a Location from the list of locations
                userInputModel.GeoResult = GetMenuValue(geoResultResponse.Results, sessionContext)!;

                if (userInputModel.GeoResult is not null)
                {
                    sessionContext.UserDetailed = await _userController.UpdateUserAsync(sessionContext.UserDetailed!.Id, userInputModel);
                }
            }
            return sessionContext;
        }

        public async Task<TContext> AllDayCardsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            ResetMenuStates(sessionContext);
            if (sessionContext.UserDetailed!.DTO_AllDayCards == null || sessionContext.UserDetailed.DTO_AllDayCards.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoDayCardsFound;
                sessionContext.UserMenuState = UserMenuState.Back;
            }
            else
            {
                sessionContext.MainHeader = sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.UserDetailed!.ToString()}\n";
                sessionContext.SubHeader = MenuText.Header.AllDayCards;

                var dayCardChoice = GetMenuValue(sessionContext!.UserDetailed!.DTO_AllDayCards!, sessionContext);

                if (dayCardChoice != null)
                {
                    sessionContext.DayCardDetailed = await _dayCardController.ReadDayCardSingleAsync(dayCardChoice.DayCardId, sessionContext.UserDetailed.Id);
                    sessionContext.DayCardMenuState = DayCardMenuState.Overview;
                }
                else
                {
                    sessionContext.UserMenuState = UserMenuState.Back;
                }


            }

            return sessionContext;
        }

        public async Task<TContext> CreateDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            ResetMenuStates(sessionContext);
            //sessionContext.MainHeader = MenuText.Prompt.CreateDayCard;
            DayCardInputModel? dayCardInputModel = View.Input_DayCard(sessionContext);

            if (dayCardInputModel != null)
            {
                sessionContext.DayCardDetailed = await _dayCardController.CreateNewDayCardAsync(sessionContext.UserDetailed!.Id, dayCardInputModel);
                sessionContext.DayCardMenuState = DayCardMenuState.Overview;

            }
            else
            {
                sessionContext.UserMenuState = UserMenuState.Back;
            }

            return sessionContext;
        }

    }
}
