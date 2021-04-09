using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class GomokuIOController
    {
        private Game.GomokuEngine gameEngine;
        private Game.Stone[,] board;

        public void StartGame() 
        {
            // Display Header
            Console.Clear();
            ConsoleIO.WriteWithColor("Welcome to Gomoku\n", System.ConsoleColor.Yellow);
            ConsoleIO.WriteWithColor("=================\n\n", System.ConsoleColor.DarkYellow);

            // Create game with 2 players of any instance type
            gameEngine = new Game.GomokuEngine(GetPlayer(1), GetPlayer(2));

            // Create an empty board
            board = new Game.Stone[Game.GomokuEngine.WIDTH, Game.GomokuEngine.WIDTH];

            // Display Starting Player
            ConsoleIO.WriteWithColor("(Randomizing)\n\n", ConsoleColor.DarkMagenta);
            ConsoleIO.WriteWithColor($"{gameEngine.Current.Name} goes first.\n", ConsoleColor.Yellow);

            // Let Player Start Game
            ConsoleIO.PromptContinue();

            // Start the game loop
            Console.Clear();
            GameLoop();
        }

        private void GameLoop() 
        {
            Game.Result result;
            do
            {
                // draw the game board
                DrawBoard();

                // Display which player's turn it is
                ConsoleIO.WriteWithColor($"{gameEngine.Current.Name}'s Turn\n", ConsoleColor.Yellow);

                // Attempt to preform a move
                Game.Stone attemptedPlay;
                do
                {
                    // get a move from the current player
                    attemptedPlay = gameEngine.Current.GenerateMove(gameEngine.Stones);

                    // Display move if no move was prompted
                    if(gameEngine.Current is not Players.HumanPlayer)
                    {
                        ConsoleIO.WriteWithColor($"Enter a row: {attemptedPlay.Row + 1}\n", ConsoleColor.DarkYellow);
                        ConsoleIO.WriteWithColor($"Enter a column: {attemptedPlay.Column + 1}\n", ConsoleColor.DarkYellow);
                    }

                    // attempt to place tile
                    result = gameEngine.Place(attemptedPlay);

                    if (!result.IsSuccess)
                    {
                        ConsoleIO.WriteWithColor("\n[Err]: " + result.Message + "\n\n", ConsoleColor.Red);
                    }

                } while (!result.IsSuccess);

                // Play successful move 
                board[attemptedPlay.Column, attemptedPlay.Row] = attemptedPlay;
                
            } while (!gameEngine.IsOver);

            // Display winning/drawing board and result message
            DrawBoard();
            ConsoleIO.WriteWithColor(result.Message + "\n", ConsoleColor.Green);
        }

        private void DrawBoard() 
        {
            Console.WriteLine();

            // Draw Top Numbering
            Console.Write("  ");
            for (int i = 1; i <= Game.GomokuEngine.WIDTH; i++) 
            {
                ConsoleIO.WriteWithColor($" {i:00}", ConsoleColor.Yellow);
            }
            Console.WriteLine();

            // Draw Board
            for (int row = 0; row < Game.GomokuEngine.WIDTH; row++) 
            {
                ConsoleIO.WriteWithColor($"{row+1:00}", ConsoleColor.Yellow);
                for (int col = 0; col < Game.GomokuEngine.WIDTH; col++) 
                {
                    if (board[col,row] == null)
                    {
                        ConsoleIO.WriteWithColor("  _", ConsoleColor.DarkYellow);
                    }
                    else 
                    {
                        char stone = board[col, row].IsBlack ? 'X' : '0';
                        ConsoleColor color = board[col, row].IsBlack ? ConsoleColor.Cyan : ConsoleColor.White;
                        ConsoleIO.WriteWithColor($"  {stone}", color);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private Players.IPlayer GetPlayer(int num) 
        {
            // Corectly Prompt Player
            ConsoleIO.WriteWithColor($"Player {num} is:\n", ConsoleColor.Yellow);
            ConsoleIO.WriteWithColor("1. Human\n", ConsoleColor.DarkYellow);
            ConsoleIO.WriteWithColor("2. Random Player\n", ConsoleColor.DarkYellow);
            
            // Get User Choice
            int choice = ConsoleIO.GetIntInRange("Select [1-2]: ", 1, 2);

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
            }

            Console.WriteLine();

            return player;
        }
    }
}
