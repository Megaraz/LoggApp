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
namespace LoggApp

{
    internal class Program
    {


        public enum MenuState { AllUsers };
        static async Task Main(string[] args)
        {

            string[] initMenu = { "[LOG IN]", "[GET ALL USERS]", "[CREATE NEW USER ACCOUNT]" };
            string[] mainDayCardMenu = { "CREATE NEW DAYCARD", "GET EXISTING DAYCARDS", "SEARCH SPECIFIC DAYCARD" };
            string[] createDateMenu = { "[T]ODAYS DATE", "[S]PECIFIED DATE" };

            //(ConsoleKeyInfo keyPress, int currentMenuIndex) = Menu.DisplayAndNavigate(initMenu, null);





            if (keyPress.Key == ConsoleKey.Enter)
            {
                switch (currentMenuIndex)
                {
                    case 0:
                        Menu.CurrentMenuState = Menu.MenuState.SpecificUser;
                        await ReadUserSingleAsync();
                        break;
                    case 1:
                        Menu.CurrentMenuState = Menu.MenuState.AllUsers;
                        await READALL_USERS();
                        break;
                    case 2:
                        await CreateNewUser();
                        Menu.CurrentMenuState = Menu.MenuState.SpecificUser;
                        break;
                }
            }







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



        private static void EDIT_DAYCARD()
        {

        }

        private static async Task ReadAllUsersAsync()
        {

            var users = await UserService.ReadAllUsersAsync();


            if (users != null && users.Count > 0)
            {
                MenuData allUsers = new MenuData()
                {
                    AllUsers = users
                };

                Menu<UserMenuDto>.CurrentMenuData.Add(allUsers);
            }
            else
            {
                Console.WriteLine("No users found");
            }


        }

        //private static void READALL_TEST()
        //{
        //    var listResult = GenericRepo<DayCard>.ReadAllDayCards();

        //    foreach (var dayCard in listResult)
        //    {
        //        Console.WriteLine(dayCard.Date);

        //        Console.WriteLine(string.Join('\n', dayCard.CaffeineDrinks));
        //        Console.WriteLine(string.Join('\n', dayCard.Activities));
        //        Console.WriteLine(string.Join('\n', dayCard.Supplements));
        //    }
        //}

        private static void READSINGLE_TEST(DateOnly date)
        {

            DayCard DayCard = GenericRepo<DayCard>.ReadDayCard_Single(date);

            Console.Clear();
            Console.WriteLine("Hämtad DayCard: ");
            Console.WriteLine($"ID: {DayCard.Id}\tDatum: {DayCard.Date}");
            Console.WriteLine();
            Console.WriteLine("Add medications");
        }

        private static async Task ReadUserSingleAsync(int id)
        {
            User user = await UserService.ReadSingleUserAsync(id)!;

            if (user == null)
            {
                Console.Clear();
                Console.WriteLine("No user found, try again");
                Thread.Sleep(1500);
                await ReadUserSingleAsync();
                return;
            }
        }

        private static async Task ReadUserSingleAsync(string? username = null)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = GetValidUserInput("Username");

            }
            User user = await UserService.ReadSingleUserAsync(username)!;

            if (user == null)
            {
                Console.Clear();
                Console.WriteLine("No user found, try again");
                Thread.Sleep(1500);
                await ReadUserSingleAsync();
                return;
            }

            Console.Clear();
            Console.WriteLine(user.ToString());

            List<string> specificUserMenu = new List<string>(){ "[CREATE NEW DAYCARD]", "[SCROLL EXISTING DAYCARDS]" };

            if (user.DayCards != null && user.DayCards.Count > 0)
            {
                //List<string> dayCardList = new List<string>();
                foreach (var card in user.DayCards)
                {
                    specificUserMenu.Add(card.Date.ToString());
                }

                //Menu.DisplayAndNavigate(dayCardList);

            }
            (ConsoleKeyInfo keyPress, int currentMenuIndex) = Menu.DisplayAndNavigate(specificUserMenu, user);

            if (keyPress.Key == ConsoleKey.Enter)
            {
                switch (currentMenuIndex)
                {
                }
            }



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





        private static void CREATE_DAYCARD_TEST(DateOnly date)
        {
            DayCard DayCard = new DayCard()
            {
                Date = date
            };
            GenericRepo<DayCard>.Add(DayCard);
            Console.WriteLine("Dag tillagd med datum: " + date.ToString());
            Thread.Sleep(1200);
            Console.Clear();
        }
    }
}
