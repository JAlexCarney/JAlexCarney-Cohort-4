using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku
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

        /// <summary>
        /// Waits for user to enter any key
        /// </summary>
        public static void PromptContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
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
        /// Gets a bool from the user by forcing them to enter either Y or N
        /// </summary>
        /// <param name="prompt">The prompt that the user sees</param>
        /// <returns></returns>
        public static bool GetYesNo(string prompt)
        {
            string userString;
            do
            {
                userString = GetString(prompt).ToUpper();
            }
            while (string.IsNullOrEmpty(userString) && 
                (userString == "Y" || userString == "N"));

            if (userString == "Y")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prompts the user to select the implementation of a player
        /// </summary>
        /// <param name="num">either 1 or 2, used for user prompting</param>
        /// <returns>A Player object of the desired implementation</returns>
        public static Players.IPlayer GetPlayer(int num)
        {
            // Corectly Prompt Player
            ConsoleIO.WriteWithColor($"Player {num} is:\n", ConsoleColor.Yellow);
            ConsoleIO.WriteWithColor("1. Human\n", ConsoleColor.DarkYellow);
            ConsoleIO.WriteWithColor("2. Random Player\n", ConsoleColor.DarkYellow);
            ConsoleIO.WriteWithColor("3. Blocking Player\n", ConsoleColor.DarkYellow);

            // Get User Choice
            int choice = ConsoleIO.GetIntInRange("Select [1-3]: ", 1, 3);

            // Create Correct Player Instance
            Players.IPlayer player = null;
            switch (choice)
            {
                case 1:
                    // Get Player Name from user
                    string name = ConsoleIO.GetString($"\nPlayer {num}, enter your name: ");
                    player = new Players.HumanPlayer(name);
                    break;
                case 2:
                    player = new Players.RandomPlayer();
                    break;
                case 3:
                    player = new Players.BlockingPlayer();
                    break;
            }

            Console.WriteLine();

            return player;
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
            }
            while (!int.TryParse(input, out output) || output < min || output > max);

            return output;
        }
    }
}
