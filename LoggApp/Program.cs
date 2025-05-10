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

            string[] createDateMenu = { "[T]ODAYS DATE", "[S]PECIFIED DATE" };

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

        private static async Task ReadAllUsersAsync()
        {

            var users = await UserService.ReadAllUsersAsync();


            if (users != null && users.Count > 0)
            {
                
            }
            else
            {
                Console.WriteLine("No users found");
            }


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
