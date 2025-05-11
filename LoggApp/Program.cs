using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Identity.Client;
using System.Text.Json;
using System.Net.Http.Json;
using Presentation;
using AppLogic;
using AppLogic.Services;
using DataAccess.Repositories;
using BusinessLogic.Models;
using AppLogic.DTOs;
using BusinessLogic.Models.Weather;
namespace LoggApp

{
    internal class Program
    {
        public static readonly string s_InitMenuHeader = "WELCOME TO LOGGAPP!";
        public static List<string> s_InitMenu = new() { "[LOG IN]", "[GET ALL USERS]", "[CREATE NEW USER ACCOUNT]" };


        public static readonly string s_SpecificUserMenuHeader = "CHOOSE ACTION FOR USER";
        public static List<string> s_SpecificUserMenu = new() { "[CREATE NEW DAYCARD]", "[SEARCH DAYCARD]" };

        public static readonly string s_CreateDayCard_Date = "ENTER A DATE FOR THE NEW DAYCARD, OR LEAVE EMPTY FOR TODAYS DATE";
        public static User? CurrentUser { get; set; }

        public static readonly string s_AllUsersMenuHeader = "ALL USERS IN DB";
        public static List<AllUserMenuDto>? AllUsers { get; set; }
        public enum MenuState { InitMenu, AllUsers, SpecificUser, CreateNewUser, CreateNewDayCard };
        public static MenuState CurrentMenuState { get; set; } = MenuState.InitMenu;

        public static string Header = s_InitMenuHeader;

        static async Task Main(string[] args)
        {


            int currentIndex = 1;
            ConsoleKeyInfo keyPress;

            do
            {

                switch (CurrentMenuState)
                {
                    case MenuState.InitMenu:
                        keyPress = MenuHandler(s_InitMenu, ref currentIndex);
                        if (keyPress.Key == ConsoleKey.Enter)
                        {
                            switch (currentIndex)
                            {
                                case 1:
                                    string username = GetValidUserInput("Username");
                                    await ReadUserSingleAsync(username);
                                    CurrentMenuState = MenuState.SpecificUser;
                                    break;
                                case 2:
                                    AllUsers = await ReadAllUsersAsync()!;
                                    CurrentMenuState = MenuState.AllUsers;
                                    break;
                                case 3:
                                    await CreateNewUser();
                                    CurrentMenuState = MenuState.SpecificUser;
                                    break;
                            }
                        }
                        break;
                    case MenuState.SpecificUser:
                        Header = $"{CurrentUser!.ToString()}";
                        foreach (var daycard in CurrentUser!.DayCards!)
                        {
                            s_SpecificUserMenu.Add(daycard.Date.ToString());
                        }

                        keyPress = MenuHandler(s_SpecificUserMenu, ref currentIndex);
                        if (keyPress.Key == ConsoleKey.Enter)
                        {
                            //switch (currentIndex)
                            //{
                            //    case 1:
                            //        DayCard dayCard = new DayCard()
                            //        {
                            //            Date = DateOnly.FromDateTime(DateTime.Today)
                            //        };
                            //        WeatherRepo weatherRepo = new WeatherRepo();
                            //        string weatherResultString = await weatherRepo.GetWeatherDataAsync(dayCard);
                            //        dayCard.WeatherData = JsonSerializer.Deserialize<List<WeatherData>>(weatherResultString);
                            //        CurrentUser.DayCards.Add(dayCard);

                            //}
                        }
                        break;

                    case MenuState.AllUsers:
                        Header = s_AllUsersMenuHeader;
                        var users = await ReadAllUsersAsync()!;
                        keyPress = MenuHandler(users, ref currentIndex);
                        if (keyPress.Key == ConsoleKey.Enter)
                        {
                            await ReadUserSingleAsync(currentIndex);
                            CurrentMenuState = MenuState.SpecificUser;
                        }
                        break;
                }


            }
            while (true);


        }

        private static ConsoleKeyInfo MenuHandler<T>(ICollection<T> currentMenu, ref int currentIndex)
        {
            Console.Clear();
            Console.WriteLine(Header);
            Console.WriteLine();
            ConsoleKeyInfo keyPress;
            currentIndex = DisplayMenu(currentMenu, currentIndex);

            keyPress = Console.ReadKey(true);

            currentIndex = InputHandler(keyPress, currentIndex);
            return keyPress;
        }

        private static int DisplayMenu<T>(ICollection<T> currentMenu, int currentIndex)
        {
            

            if (currentMenu != null && currentMenu.Count > 0)
            {
                if (currentIndex > currentMenu.Count)
                {
                    currentIndex = 1;
                }
                if (currentIndex < 1)
                {
                    currentIndex = currentMenu.Count;
                }
                int i = 1;
                foreach (var item in currentMenu)
                {
                    if (currentIndex == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(item);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(item);
                    }
                    i++;
                }
            }
            return currentIndex;
        }

        public static int InputHandler(ConsoleKeyInfo keyPress, int currentIndex)
        {
            switch (keyPress.Key)
            {
                case ConsoleKey.DownArrow:
                    ++currentIndex;
                    break;
                case ConsoleKey.UpArrow:
                    --currentIndex;
                    break;
                case ConsoleKey.LeftArrow:
                    --currentIndex;
                    break;
                case ConsoleKey.RightArrow:
                    ++currentIndex;
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Enter:
                    break;
                default: break;
            }
            return currentIndex;
        }
        
        private static async Task CreateNewDayCard()
        {

        }

        private static async Task CreateNewUser()
        {

            var input = new UserInputModel()
            {
                Username = GetValidUserInput("Username"),
                CityName = GetValidUserInput("City")
            };

            await UserService.RegisterNewUserAsync(input);

            await ReadUserSingleAsync(input.Username);
        }

        private static async Task<List<AllUserMenuDto>>? ReadAllUsersAsync()
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

        private static async Task ReadUserSingleAsync(int id)
        {
            CurrentUser = await UserService.ReadSingleUserAsync(id)!;

            //if (user == null)
            //{
            //    Console.Clear();
            //    Console.WriteLine("No user found, try again");
            //    Thread.Sleep(1500);
            //    await ReadUserSingleAsync(id);
            //    return;
            //}

        }

        private static async Task ReadUserSingleAsync(string username)
        {

            CurrentUser = await UserService.ReadSingleUserAsync(username)!;

            //if (user == null)
            //{
            //    Console.Clear();
            //    Console.WriteLine("No user found, try again");
            //    Thread.Sleep(1500);
            //    await ReadUserSingleAsync(username);
            //    return;
            //}

        }



        private static string GetValidUserInput(string typeOfEntry)
        {
            Console.Clear();
            Console.WriteLine($"Enter a {typeOfEntry}: ");
            string userInput;
            bool validInput;

            do
            {

                userInput = Console.ReadLine()!;
                validInput = !string.IsNullOrWhiteSpace(userInput) && !string.IsNullOrEmpty(userInput);

                if (!validInput)
                {
                    Console.Clear();
                    Console.WriteLine($"Enter a valid {typeOfEntry} (not empty).");
                }

            }
            while (!validInput);

            return userInput;
        }

    }
}
