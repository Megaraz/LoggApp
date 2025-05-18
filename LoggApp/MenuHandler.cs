using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using AppLogic.Models.DTOs;
using AppLogic.Models;
using AppLogic.Services;
using DataAccess;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.Models;
using AppLogic.Controllers;
using Microsoft.EntityFrameworkCore;
using Presentation.MenuState_Enums;

namespace Presentation
{
    public class MenuHandler
    {

        private readonly Controller _controller;

        public MenuHandler(Controller controller)
        {
            _controller = controller;
        }



        public async Task<TContext> HandleUserMenuState<TContext>(TContext sessionContext) where TContext : SessionContext
        {


            switch (sessionContext.CurrentUserMenuState)
            {
                case InitUserMenuState.AllDayCards:
                    sessionContext = await AllDayCardsMenuHandler(sessionContext);
                    break;

                case InitUserMenuState.CreateNewDayCard:
                    sessionContext = await CreateDayCardMenuHandler(sessionContext);
                    break;

                case InitUserMenuState.SpecificDayCard:
                    sessionContext = SpecificDayCardMenuHandler(sessionContext);
                    break;


                case InitUserMenuState.SearchDayCard:

                    break;
            }

            return sessionContext;

        }


        public async Task<TContext> HandleMainMenuState<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            switch (sessionContext.CurrentInitMenuState)
            {
                case InitMainMenuState.Main:
                    sessionContext = await InitMenuHandler(sessionContext);
                    break;

                case InitMainMenuState.SpecificUser:
                    sessionContext = await SpecificUserMenuHandler(sessionContext);
                    break;

                case InitMainMenuState.AllUsers:
                    sessionContext = await AllUsersMenuHandler(sessionContext);
                    break;

                case InitMainMenuState.Exit:
                    break;

                
            }
            return sessionContext;
        }
        //public async Task<TContext> HandleCurrentState<TContext>(TContext sessionContext) where TContext : SessionContext
        //{

        //    switch (sessionContext.CurrentMenuState)
        //    {
        //        case MenuState.InitMenu:
        //            sessionContext = await InitMenuHandler(sessionContext);
        //            break;

        //        case MenuState.SpecificUser:
        //            sessionContext = SpecificUserMenuHandler(sessionContext);
        //            break;

        //        case MenuState.AllDayCards:
        //            sessionContext = await AllDayCardsMenuHandler(sessionContext);
        //            break;

        //        case MenuState.AllUsers:
        //            sessionContext = await AllUsersMenuHandler(sessionContext);
        //            break;

        //        case MenuState.CreateNewDayCard:
        //            sessionContext = await CreateDayCardMenuHandler(sessionContext);
        //            break;

        //        case MenuState.SpecificDayCard:
        //            sessionContext = SpecificDayCardMenuHandler(sessionContext);
        //            break;

                
        //        case MenuState.SearchDayCard:

        //            break;
        //    }
        //    return sessionContext;
        //}

        public async Task<TContext> InitMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            var initMenuChoice = GetMenuValue(sessionContext.CurrentMainMenu!, sessionContext);
            switch (initMenuChoice)
            {
                case MenuText.NavOption.Login:
                    sessionContext = await Login(sessionContext);

                    break;

                case MenuText.NavOption.GetAllUsers:
                    sessionContext = await GetAllUsers(sessionContext);
                    break;

                case MenuText.NavOption.CreateNewUser:
                    sessionContext = await CreateNewUser(sessionContext);
                    break;

                case MenuText.NavOption.Exit:
                    sessionContext.CurrentInitMenuState = InitMainMenuState.Exit;
                    break;
            }

            return sessionContext;
        }

        private async Task<TContext> CreateNewUser<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            // Get basic user info/input
            UserInputModel userInputModel = View.Input_User();
            // Get list of locations that match users input location
            GeoResultResponse geoResultResponse = await _controller.UserGeoResultList(userInputModel);
            // User chooses a Location from the list of locations
            userInputModel.GeoResult = GetMenuValue(geoResultResponse.Results, sessionContext);
            // Create a new user with the updated GeoResult, and sets the CurrentUser to the newly created one
            sessionContext.DTO_CurrentUser = await _controller.CreateNewUserAsync(userInputModel);
            sessionContext.CurrentInitMenuState = InitMainMenuState.SpecificUser;

            return sessionContext;
        }

        private async Task<TContext> GetAllUsers<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            List<DTO_AllUser>? allUsers = await _controller.ReadAllUsersAsync()!;

            if (allUsers == null || allUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUserFound;
                sessionContext.CurrentInitMenuState = InitMainMenuState.Main;
            }
            else
            {
                sessionContext.CurrentInitMenuState = InitMainMenuState.AllUsers;
                sessionContext.CurrentMainMenu = MenuText.NavOption.s_AllUserMenu.ToList();
                sessionContext.DTO_AllUsers = allUsers;

            }

            return sessionContext;
        }

        private async Task<TContext> Login<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            string username = View.GetValidUserInput(MenuText.Prompt.CreateUser, MenuText.Error.InvalidUserNameInput);

            DTO_SpecificUser? resultUser = await _controller.ReadUserSingleAsync(username);

            if (resultUser == null)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUserFound;
                sessionContext.CurrentInitMenuState = InitMainMenuState.Main;

            }
            else
            {
                sessionContext.DTO_CurrentUser = resultUser;
                sessionContext.CurrentInitMenuState = InitMainMenuState.SpecificUser;
            }
            return sessionContext;
        }

        public TContext SpecificDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {

            sessionContext.MainHeader = MenuText.Header.CurrentDayCard;
            //Console.WriteLine($"{sessionContext.DTO_CurrentDayCard.ToString()}");
            //Console.ReadLine();
            return sessionContext;

        }

        public async Task<TContext> CreateDayCardMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.MainHeader = MenuText.Prompt.CreateDayCard;
            DayCardInputModel dayCardInputModel = View.Input_DayCard(sessionContext);
            await _controller.CreateNewDayCardAsync(dayCardInputModel);
            sessionContext.CurrentUserMenuState = InitUserMenuState.SpecificDayCard;

            return sessionContext;
        }

        public async Task<TContext> AllUsersMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            if (sessionContext.DTO_AllUsers == null || sessionContext.DTO_AllUsers.Count == 0)
            {
                sessionContext.ErrorMessage = MenuText.Error.NoUsersFound;
                sessionContext.CurrentInitMenuState = InitMainMenuState.Main;

            }
            else
            {
                sessionContext.MainHeader = MenuText.Header.AllUsers;

                var userChoice = GetMenuValue(sessionContext.DTO_AllUsers, sessionContext);

                sessionContext
                    .DTO_CurrentUser = await _controller.ReadUserSingleAsync(userChoice.Id);

                sessionContext
                    .MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}\n\n\n";

                sessionContext
                    .CurrentInitMenuState = InitMainMenuState.SpecificUser;

            }
            return sessionContext;
        }

        public async Task<TContext> AllDayCardsMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            if (sessionContext.DTO_CurrentUser!.DTO_AllDayCards == null || sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count == 0)
            {
                sessionContext
                    .ErrorMessage = MenuText.Error.NoDayCardsFound;
                sessionContext
                    .CurrentInitMenuState = InitMainMenuState.SpecificUser;
            }
            else
            {
                sessionContext
                .SubHeader = MenuText.Header.AllDayCards;

                var dayCardChoice = GetMenuValue(sessionContext!.DTO_CurrentUser!.DTO_AllDayCards!, sessionContext);


                sessionContext.DTO_CurrentDayCard = await _controller.ReadDayCardSingleAsync(dayCardChoice.DayCardId, sessionContext.DTO_CurrentUser.Id);
                sessionContext.CurrentUserMenuState = InitUserMenuState.SpecificDayCard;
            }


            return sessionContext;
        }

        public async Task<TContext> SpecificUserMenuHandler<TContext>(TContext sessionContext) where TContext : SessionContext
        {
            sessionContext.MainHeader = MenuText.Header.SpecificUser + $"{sessionContext.DTO_CurrentUser!.ToString()}";


            if (sessionContext.DTO_CurrentUser.DTO_AllDayCards!.Count > 0)
            {
                sessionContext.SubHeader = "Number of Daycards: " + sessionContext.DTO_CurrentUser.DTO_AllDayCards.Count;

            }


            var specUserChoice = GetMenuValue(MenuText.NavOption.s_SpecificUserMenu.ToList(), sessionContext);
            switch (specUserChoice)
            {
                case MenuText.NavOption.CreateNewDayCard:
                    sessionContext.CurrentUserMenuState = InitUserMenuState.CreateNewDayCard;
                    break;
                case MenuText.NavOption.ShowAllDayCards:
                    sessionContext.CurrentUserMenuState = InitUserMenuState.AllDayCards;
                    break;
                case MenuText.NavOption.SearchDayCard:
                    sessionContext.CurrentUserMenuState = InitUserMenuState.SearchDayCard;
                    break;
            }
            return await HandleUserMenuState(sessionContext);
        }

        public T GetMenuValue<T>(List<T> currentMenu, SessionContext sessionContext)
        {
            List<string> currentMenuStringList = currentMenu.Select(x => x?.ToString()).ToList()!;


            ConsoleKey keyPress;
            int currentIndex = 0;
            do
            {

                View.DisplayMenu(currentMenuStringList, ref currentIndex, sessionContext.MainHeader!, sessionContext.SubHeader, sessionContext.ErrorMessage);
                keyPress = View.InputToMenuIndex(ref currentIndex);

            } while (keyPress != ConsoleKey.Enter && keyPress != ConsoleKey.Escape);


            return currentMenu[currentIndex]!;
        }

    }

}
