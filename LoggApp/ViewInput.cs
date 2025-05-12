using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    internal class ViewInput
    {

        public static string GetValidUserInput(string typeOfEntry)
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
        public static void InputHandler(ConsoleKeyInfo keyPress, ref int currentIndex)
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

        }
    }
}
