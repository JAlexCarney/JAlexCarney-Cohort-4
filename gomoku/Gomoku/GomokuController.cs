using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class GomokuController
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
            gameEngine = new Game.GomokuEngine(ConsoleIO.GetPlayer(1), ConsoleIO.GetPlayer(2));

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
                GomokuDisplayer.DrawBoard(board);

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
            GomokuDisplayer.DrawBoard(board);
            ConsoleIO.WriteWithColor(result.Message + "\n", ConsoleColor.Green);
        }
    }
}
