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
            var panel = new SolarPanel
            {
                Section = GetString("Section: "),
                Row = GetIntInRange("Row: ", 1, 250),
                Column = GetIntInRange("Column: ", 1, 250),
                Material = GetSolarPanelMarterial("Material: "),
                YearInstalled = new DateTime(GetIntInRange("Installation Year: ", 1, DateTime.Now.Year), 1, 1),
                IsTracking = GetYesOrNo("Tracked [y/n]: ")
            };
            Console.WriteLine();
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

        public static SolarPanel UpdatePanel(SolarPanel originalPanel)
        {
            DisplayUpdatingPanel(originalPanel);

            string originalYesOrNo = originalPanel.IsTracking ? "yes" : "no";

            var panel = new SolarPanel
            {
                Section = GetStringDefualtable($"Section ({originalPanel.Section}): ", originalPanel.Section),
                Row = GetIntInRangeDefualtable($"Row ({originalPanel.Row}): ", 1, 250, originalPanel.Row),
                Column = GetIntInRangeDefualtable($"Column ({originalPanel.Column}): ", 1, 250, originalPanel.Column),
                Material = GetSolarPanelMaterialDefualtable($"Material ({originalPanel.Material}): ", originalPanel.Material),
                YearInstalled = new DateTime(GetIntInRangeDefualtable($"Installation Year ({originalPanel.YearInstalled.Year}): ", 1, DateTime.Now.Year, originalPanel.YearInstalled.Year), 1, 1),
                IsTracking = GetYesOrNoDefualtable($"Tracked ({originalYesOrNo}) [y/n]: ", originalPanel.IsTracking)
            };
            Console.WriteLine();
            return panel;
        }

        private static bool GetYesOrNoDefualtable(string prompt, bool defualt)
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
                        DisplayFailure("Must enter y, n or [Enter].");
                    }
                }
                else
                {
                    return defualt;
                }
            }
            while (true);
        }

        public static SolarPanelMaterial GetSolarPanelMaterialDefualtable(string prompt, SolarPanelMaterial defualt) 
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
                if (string.IsNullOrEmpty(input)) 
                {
                    return defualt;
                }
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

        public static int GetIntInRangeDefualtable(string prompt, int min, int max, int defualt) 
        {
            int output;
            string input;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (string.IsNullOrEmpty(input)) 
                {
                    return defualt;
                }
                if (int.TryParse(input, out output))
                {
                    if (output < min || output > max)
                    {
                        DisplayFailure($"Must be between {min} and {max}.");
                    }
                }
                else
                {
                    DisplayFailure("Must enter an integer.");
                }
            }
            while (!int.TryParse(input, out output) || output < min || output > max);

            return output;
        }

        public static string GetStringDefualtable(string prompt, string defualt) 
        {
            string output;
            
            Console.Write(prompt);
            Console.ForegroundColor = ConsoleColor.Cyan;
            output = Console.ReadLine().Trim();
            Console.ForegroundColor = ConsoleColor.White;

            if (string.IsNullOrEmpty(output)) 
            {
                return defualt;
            }

            return output;
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

        public static void DisplayUpdatingPanel(SolarPanel panel) 
        {
            ConsoleIO.WriteWithColor($"Editing {panel}\nPress [Enter] to keep original value.\n\n", ConsoleColor.DarkYellow);
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
                output = Console.ReadLine().Trim();
                Console.ForegroundColor = ConsoleColor.White;
                if (string.IsNullOrEmpty(output)) 
                {
                    DisplayFailure("Entry can not be empty.");
                }
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
            int output;
            string input;
            do
            {
                Console.Write(prompt);
                Console.ForegroundColor = ConsoleColor.Cyan;
                input = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (int.TryParse(input, out output))
                {
                    if (output < min || output > max) 
                    {
                        DisplayFailure($"Must be between {min} and {max}.");
                    }
                }
                else 
                {
                    DisplayFailure("Must enter an integer.");
                }
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
