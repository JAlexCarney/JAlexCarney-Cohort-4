using System;
using System.Collections.Generic;
using System.Text;
using SolarFarm.Core;

namespace SolarFarm.UI
{
    public static class ConsoleIO
    {
        /// <summary>
        /// Displays a string using a color instead of just white
        /// </summary>
        /// <param name="toWrite">The string to display</param>
        /// <param name="color">The color to display it in</param>
        public static void WriteWithColor(string toWrite, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(toWrite);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static SolarPanel GetSolarPanel() 
        {
            var panel = new SolarPanel();
            panel.Section = GetString("Section: ");
            panel.Row = GetIntInRange("Row: ", 1, 250);
            panel.Column = GetIntInRange("Column: ", 1, 250);
            panel.Material = GetSolarPanelMarterial("Material: ");
            panel.YearInstalled = new DateTime(GetIntInRange("Installation Year: ", 1, DateTime.Now.Year), 1, 1);
            panel.IsTracking = GetYesOrNo("Tracked [y/n]: ");
            return panel;
        }

        private static bool GetYesOrNo(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine().ToLower();
                Console.ForegroundColor = ConsoleColor.White;
                if (!string.IsNullOrEmpty(input))
                {
                    if (input == "y")
                    {
                        return true;
                    }
                    else if (input == "n")
                    {
                        return false;
                    }
                    else 
                    {
                        DisplayFailure("Must enter y or n.");
                    }
                }
                else
                {
                    DisplayFailure("Field is Required!");
                }
            }
            while (true);
        }

        private static SolarPanelMaterial GetSolarPanelMarterial(string prompt)
        {
            SolarPanelMaterial output = SolarPanelMaterial.AmoSi;
            string input;
            bool validInput;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                validInput = Enum.IsDefined(typeof(SolarPanelMaterial), input);
                if (validInput)
                {
                    // PlayerChoice.Rock
                    output = Enum.Parse<SolarPanelMaterial>(input);
                }
                else 
                {
                    DisplayFailure("Material is not valid.");
                }
            }
            while (!validInput);
            return output;
        }

        /// <summary>
        /// Waits for user to enter any key
        /// </summary>
        public static void PromptContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void DisplaySuccess(string prompt) 
        {
            WriteWithColor("[Success]\n", ConsoleColor.Green);
            WriteWithColor(prompt, ConsoleColor.Green);
            Console.WriteLine();
        }

        public static void DisplayFailure(string prompt) 
        {
            WriteWithColor("[Err]\n", ConsoleColor.Red);
            WriteWithColor(prompt, ConsoleColor.Red);
            Console.WriteLine();
        }

        public static void DisplaySolarPanelList(List<SolarPanel> panels) 
        {
            Console.WriteLine();
            WriteWithColor("Row Col Year Material Tracking\n", ConsoleColor.Yellow);
            foreach (SolarPanel panel in panels) 
            {
                WriteWithColor($"{panel.Row,3} {panel.Column,3} {panel.YearInstalled.Year,4} {panel.Material,8}", ConsoleColor.DarkYellow);
                if (panel.IsTracking)
                {
                    WriteWithColor("      yes", ConsoleColor.DarkGreen);
                }
                else 
                {
                    WriteWithColor("       no", ConsoleColor.DarkRed);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prompts the user for a string and returns once the user enters a valid one
        /// </summary>
        /// <param name="prompt">The prompt that the user sees</param>
        /// <returns>The first valid string that the user enters</returns>
        public static string GetString(string prompt)
        {
            string output;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                output = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            while (string.IsNullOrEmpty(output));
            return output;
        }

        /// <summary>
        /// Display a numbered list of options
        /// </summary>
        /// <param name="options">any number of options to display</param>
        public static void DisplayMenuOptions(params string[] options) 
        {
            WriteWithColor(options[0], ConsoleColor.Yellow);
            Console.WriteLine();
            WriteWithColor(new string('=', options[0].Length), ConsoleColor.DarkYellow);
            Console.WriteLine();
            for (int i = 1; i < options.Length; i++)
            {
                WriteWithColor($"{i-1}. {options[i]}\n", ConsoleColor.DarkYellow);
            }
        }

        public static void DisplayHeader(string title) 
        {
            Console.Clear();
            WriteWithColor(title, ConsoleColor.Yellow);
            Console.WriteLine();
            WriteWithColor(new string('=', title.Length), ConsoleColor.DarkYellow);
            Console.WriteLine("\n");
        }

        /// <summary>
        /// repeatedly Prompts the user for an int and only returns once they have
        /// entered a valid int in the given range
        /// </summary>
        /// <param name="prompt">The prompt shown to the user</param>
        /// <param name="min">The smallest valid int</param>
        /// <param name="max">The largest valid int</param>
        /// <returns>The first valid int entered by the user</returns>
        public static int GetIntInRange(string prompt, int min, int max)
        {
            int output = min - 1;
            string input;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            while (!int.TryParse(input, out output) || output < min || output > max);

            return output;
        }

        /// <summary>
        /// Gets a date and/or time from the user
        /// </summary>
        /// <param name="prompt">The prompt shown to the user</param>
        /// <returns>The time the user entered</returns>
        public static DateTime GetDateTime(string prompt)
        {
            string userString;
            DateTime output;
            do
            {
                Console.Write(prompt);
                userString = Console.ReadLine();
            }
            while (!DateTime.TryParse(userString, out output));
            return output;
        }

        /// <summary>
        /// Gets a past date and/or time from the user
        /// </summary>
        /// <param name="prompt">The prompt shown to the user</param>
        /// <returns>The time the user entered</returns>
        public static DateTime GetPastDateTime(string prompt)
        {
            DateTime output;
            do
            {
                output = GetDateTime(prompt);
            }
            while (output.Subtract(DateTime.Now).Ticks >= 0);
            return output;
        }

        /// <summary>
        /// Gets a future date and/or time from the user
        /// </summary>
        /// <param name="prompt">The prompt shown to the user</param>
        /// <returns>The time the user entered</returns>
        public static DateTime GetFutureDateTime(string prompt)
        {
            DateTime output;
            do
            {
                output = GetDateTime(prompt);
            }
            while (output.Subtract(DateTime.Now).Ticks <= 0);
            return output;
        }

        /// <summary>
        /// repeatedly Prompts the user for an decimal and only returns once they have
        /// entered a valid int in the given range
        /// </summary>
        /// <param name="prompt">The prompt shown to the user</param>
        /// <param name="min">The smallest valid decimal</param>
        /// <param name="max">The largest valid decimal</param>
        /// <returns>The first valid int entered by the user</returns>
        public static decimal GetDecimalInRange(string prompt, decimal min, decimal max)
        {
            decimal output;
            string input;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            while (!decimal.TryParse(input, out output) || output < min || output > max);

            return output;
        }

        /// <summary>
        /// repeatedly Prompts the user for an int and only returns once they have
        /// entered a valid int in the given range
        /// </summary>
        /// <param name="prompt">The prompt shown to the user</param>
        /// <param name="min">The smallest valid int</param>
        /// <param name="max">The largest valid int</param>
        /// <returns>The first valid int entered by the user</returns>
        public static double GetDoubleInRange(string prompt, double min, double max)
        {
            double output;
            string input;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            }
            while (!double.TryParse(input, out output) || output < min || output > max);

            return output;
        }

        public static void DrawHorse(int x, int y, ConsoleColor color) 
        {
            Console.SetCursorPosition(x, y);
            WriteWithColor("   _____,,;;;`;", color);
            Console.SetCursorPosition(x, y+1);
            WriteWithColor(",~(  )  , )~~\\|", color);
            Console.SetCursorPosition(x, y+2);
            WriteWithColor("' / / --`--,   ", color);
            Console.SetCursorPosition(x, y+3);
            WriteWithColor(" /  \\    | '   ", color);
        }

        public static void PromptContinueAndClear() 
        {
            PromptContinue();
            Console.Clear();
        }
    }
}
