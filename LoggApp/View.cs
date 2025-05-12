using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using AppLogic.DTOs;
using AppLogic.Models;
using AppLogic.Services;

namespace Presentation
{
    internal class View
    {
        public async Task Start()
        {
            do
            {

                switch (Menu.CurrentMenuState)
                {
                    case MenuState.InitMenu:
                        await Menu.InitMenuHandler();
                        break;
                    case MenuState.SpecificUser:
                        //Header = $"USER ID\t\tUSERNAME\tCITY\n\n{CurrentUserMenuDto!.ToString()}\n\n\nDAYCARD ID\tDATE";
                        var specUserChoice = Menu.BaseMenuHandler(MenuData.s_SpecificUserMenu);
                        switch (specUserChoice)
                        {
                            case "[CREATE NEW DAYCARD]":
                                Menu.CurrentMenuState = MenuState.CreateNewDayCard;
                                break;
                            case "[SHOW ALL DAYCARDS]":
                                Menu.CurrentMenuState = MenuState.AllDayCards;
                                break;
                            case "[SEARCH DAYCARD]":
                                Menu.CurrentMenuState = MenuState.SearchDayCard;
                                break;
                        }
                        break;

                    case MenuState.AllDayCards:
                        var dayCardChoice = Menu.BaseMenuHandler(MenuData.CurrentUserMenu!.AllDayCardsMenu!);
                        await ReadDayCardSingleAsync(dayCardChoice.DayCardId, MenuData.CurrentUserMenu.Id);
                        Menu.CurrentMenuState = MenuState.SpecificDayCard;
                        break;

                    case MenuState.AllUsers:
                        if (MenuData.AllUsersMenu == null || MenuData.AllUsersMenu.Count == 0)
                        {
                            MenuData.PageHeader = "NO USERS FOUND";
                            Menu.CurrentMenuState = MenuState.InitMenu;
                            goto case MenuState.InitMenu;
                        }
                        else
                            MenuData.PageHeader = MenuData.s_AllUsersMenuHeader;

                        var choice = Menu.BaseMenuHandler(MenuData.AllUsersMenu);
                        await ReadUserSingleAsync(choice.Id);
                        Menu.CurrentMenuState = MenuState.SpecificUser;
                        break;

                    case MenuState.CreateNewDayCard:
                        MenuData.PageHeader = MenuData.s_CreateDayCardHeader;
                        await CreateNewDayCardAsync();
                        Menu.CurrentMenuState = MenuState.SpecificDayCard;
                        break;

                    case MenuState.SpecificDayCard:
                        MenuData.PageHeader = $"CURRENT DAYCARD:\n";
                        Console.WriteLine(MenuData.CurrentDayCardMenu!.WeatherSummary!.ToString());
                        Console.WriteLine(MenuData.CurrentDayCardMenu!.AirQualitySummary!.ToString());
                        Console.ReadLine();

                        break;

                }


            }
            while (true);

        }

        public static async Task CreateNewDayCardAsync()
        {
            var input = new DayCardInputModel()
            {
                UserId = MenuData.CurrentUserMenu!.Id,
                Date = DateOnly.Parse(ViewInput.GetValidUserInput("Date")),
                Lat = MenuData.CurrentUserMenu!.Lat,
                Lon = MenuData.CurrentUserMenu!.Lon
            };

            await DayCardService.CreateNewDayCardAsync(input);

            await ReadDayCardSingleAsync((DateOnly)input.Date, MenuData.CurrentUserMenu.Id);
        }

        public static async Task CreateNewUserAsync()
        {

            var input = new UserInputModel()
            {
                Username = ViewInput.GetValidUserInput("Username"),
                CityName = ViewInput.GetValidUserInput("City")
            };

            var geoResultMenu = await UserService.GetLocationsMenu(input.CityName);
            input.GeoResult = Menu.BaseMenuHandler(geoResultMenu.Results);
            MenuData.CurrentUserMenu = await UserService.RegisterNewUserAsync(input);
        }

        public static async Task<List<AllUserMenuDto>>? ReadAllUsersAsync()
        {

            var users = await UserService.ReadAllUsersAsync();


            if (users != null && users.Count > 0)
            {
                return users;
            }
            else
            {
                return null;
            }


        }

        public static async Task<List<AllDayCardsMenuDto>>? ReadAllDayCardsAsyc(int userId)
        {
            return await DayCardService.ReadAllDayCardsAsync(userId);
        }

        public static async Task ReadDayCardSingleAsync(int id, int userId)
        {
            MenuData.CurrentDayCardMenu = await DayCardService.ReadSingleDayCardAsync(id, userId)!;
        }
        public static async Task ReadDayCardSingleAsync(DateOnly date, int userId)
        {
            MenuData.CurrentDayCardMenu = await DayCardService.ReadSingleDayCardAsync(date, userId)!;
        }

        public static async Task ReadUserSingleAsync(int id)
        {
            MenuData.CurrentUserMenu = await UserService.ReadSingleUserAsync(id)!;

        }

        public static async Task ReadUserSingleAsync(string username)
        {

            MenuData.CurrentUserMenu = await UserService.ReadSingleUserAsync(username)!;

        }
    }
}
    
