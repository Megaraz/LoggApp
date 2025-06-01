using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Display
{
    internal class ConsoleViewRenderer
    {

        #region Display Methods
        // MAIN DISPLAY METHOD
        // Displays the current menu with the given index highlighted.
        public static void DisplayMenu<T>(List<T> currentMenu, ref int CurrentMenuIndex, SessionContext sessionContext)
        {
            Console.Clear();

            List<string> currentMenuStringList = currentMenu.Select(x => x?.ToString()).ToList()!;


            // Write MainHeader
            if (!sessionContext.MainHeader.IsNullOrEmpty())
            {
                Console.WriteLine(sessionContext.MainHeader + Environment.NewLine);
            }
            if (!sessionContext.CurrentPrompt.IsNullOrEmpty())
            {
                Console.WriteLine(sessionContext.CurrentPrompt + '\n');
            }
            // Write Error
            if (!sessionContext.ErrorMessage.IsNullOrEmpty())
            {
                Console.WriteLine(sessionContext.ErrorMessage + '\n');
            }
            // Write SubHeader
            if (!sessionContext.SubHeader.IsNullOrEmpty())
            {
                Console.WriteLine(sessionContext.SubHeader);
            }

            if (currentMenuStringList != null && currentMenuStringList.Count > 0)
            {
                if (CurrentMenuIndex > currentMenuStringList.Count - 1)
                {
                    CurrentMenuIndex = 0;
                }
                if (CurrentMenuIndex < 0)
                {
                    CurrentMenuIndex = currentMenuStringList.Count - 1;
                }

                for (int i = 0; i < currentMenuStringList.Count; i++)
                {

                    var item = currentMenuStringList[i];

                    if (item == MenuText.NavOption.Exit ||
                        item == MenuText.NavOption.GetTodaysWeather ||
                        item == MenuText.NavOption.Back ||
                        item.Contains("CHANGE") ||
                        item.Contains("SETTINGS")
                        )
                    {
                        item = "\n" + item;
                    }

                    if (CurrentMenuIndex == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine(item);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(item);
                    }
                }

            }
            // Draw stats window if the current menu is the init menu.
            if (currentMenu.Select(x => x?.ToString()).SequenceEqual(MenuText.NavOption.s_InitMenu))
            {

                DrawStatsWindow(sessionContext);
            }
            // Write footer
            if (!sessionContext.Footer.IsNullOrEmpty())
            {
                Console.WriteLine(sessionContext.Footer);
            }
        }

        // Draws a frame with stats within
        public static void DrawStatsWindow(SessionContext sessionContext, int windowWidth = 25, int windowTop = 3)
        {
            // Calculate left position for the stats window to be placed at the right side of the console.
            int windowLeft = Math.Max(Console.WindowWidth - windowWidth - 50, 0);

            // Prepare content lines for the stats window.
            var statsLines = new[]
            {
            "STATS",
            $"Users in DB: {sessionContext.UserCountInDb}"
            // Additional stats can be appended here in future.
        };

            // Determine window height (content lines + top & bottom borders).
            int windowHeight = statsLines.Length + 2;

            // Draw the top border.
            Console.SetCursorPosition(windowLeft, windowTop);
            Console.Write("+" + new string('-', windowWidth) + "+");

            // Draw each content line inside the window.
            for (int i = 0; i < statsLines.Length; i++)
            {
                Console.SetCursorPosition(windowLeft, windowTop + 1 + i);
                // Ensure the line is padded to fit the window width.
                string lineContent = statsLines[i].PadRight(windowWidth);
                Console.Write("|" + lineContent + "|");
            }

            // Draw the bottom border.
            Console.SetCursorPosition(windowLeft, windowTop + windowHeight - 1);
            Console.Write("+" + new string('-', windowWidth) + "+");
        }

        #endregion
    }
}
