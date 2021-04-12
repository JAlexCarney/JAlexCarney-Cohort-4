using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    public static class GomokuDisplayer
    {
        /// <summary>
        /// Uses the past move data to draw a Go board with stones
        /// </summary>
        public static void DrawBoard(Game.Stone[,] board)
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
                ConsoleIO.WriteWithColor($"{row + 1:00} ", ConsoleColor.Yellow);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                for (int col = 0; col < Game.GomokuEngine.WIDTH; col++)
                {
                    if (board[col, row] == null)
                    {
                        ConsoleIO.WriteWithColor(" - ", ConsoleColor.Gray);
                    }
                    else
                    {
                        char stone = '■';
                        ConsoleColor color = board[col, row].IsBlack ? ConsoleColor.Black : ConsoleColor.White;
                        ConsoleIO.WriteWithColor($" {stone} ", color);
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleIO.WriteWithColor(":\n", ConsoleColor.Black);
            }

            Console.WriteLine();
        }
    }
}
