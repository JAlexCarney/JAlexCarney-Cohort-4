using Gomoku.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku.Players
{
    class BlockingPlayer : IPlayer
    {
        public string Name { get; set; }

        // same names as Random Player
        private static string[] names = {
            "Evelyn", "Wyatan", "Jud", "Danella", "Sarah", "Johnna",
            "Vicki", "Alano", "Trever", "Delphine", "Sigismundo",
            "Shermie", "Filide", "Daniella", "Annmarie", "Bartram",
            "Pennie", "Rafael", "Celine", "Kacey", "Saree", "Tu",
            "Erny", "Evonne", "Charita", "Anny", "Mavra", "Fredek",
            "Silvio", "Cam", "Hulda", "Nanice", "Iolanthe", "Brucie",
            "Kara", "Paco"};

        private Random rng = new Random();

        /// <summary>
        /// Creates a Blocking player with a random name
        /// </summary>
        public BlockingPlayer() 
        {
            // let people know what this player is all about :P
            Name = names[rng.Next(0, names.Length)] + " The Blocker";
        }

        /// <summary>
        /// This player always plays near where the privous player played
        /// </summary>
        /// <param name="previousMoves">Used to get color and location of past moves</param>
        /// <returns></returns>
        public Stone GenerateMove(Stone[] previousMoves)
        {
            bool isBlack = true;
            Stone lastMove;
            // Get the previous move if it exists
            if (previousMoves != null && previousMoves.Length > 0)
            {
                lastMove = previousMoves[previousMoves.Length - 1];
                isBlack = !lastMove.IsBlack;
            }
            // if this is the first move, just play randomly
            else 
            {
                return new Stone( rng.Next(GomokuEngine.WIDTH), rng.Next(GomokuEngine.WIDTH), isBlack);
            }

            int newRow;
            int newColumn;
            bool isAvalible;
            int maxAttempts = 50;
            int attemptCount = 0;
            do
            {
                // Making an attempt
                attemptCount++;

                // pick a move in a space next to the previous move
                newRow = rng.Next(lastMove.Row - 1, lastMove.Row + 2);
                newColumn = rng.Next(lastMove.Column - 1, lastMove.Column + 2);

                // make sure it's not a duplicate move and is on the board
                isAvalible = true;
                if (newRow >= 0 && newRow < Game.GomokuEngine.WIDTH && newColumn >= 0 && newColumn < Game.GomokuEngine.WIDTH)
                {
                    for (int i = 0; i < previousMoves.Length; i++)
                    {
                        if (newRow == previousMoves[i].Row && newColumn == previousMoves[i].Column)
                        {
                            isAvalible = false;
                            break;
                        }
                    }
                }
                else 
                {
                    isAvalible = false;
                }
            }
            // Stop looping if a space is found OR too many attempts are made
            while (isAvalible == false && attemptCount < maxAttempts);

            // if no valid move was found, play randomly
            if (attemptCount >= maxAttempts) 
            {
                return new Stone(rng.Next(GomokuEngine.WIDTH), rng.Next(GomokuEngine.WIDTH), isBlack);
            }

            // otherwise return the found valid move
            return new Stone(newRow, newColumn, isBlack);
        }
    }
}
