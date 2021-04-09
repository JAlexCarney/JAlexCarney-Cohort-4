using Gomoku.Game;

namespace Gomoku.Players
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; private set; }       

        public HumanPlayer(string name)
        {
            Name = name;
        }

        public Stone GenerateMove(Stone[] previousMoves)
        {
            bool isBlack = true;
            if (previousMoves != null && previousMoves.Length > 0)
            {
                Stone lastMove = previousMoves[previousMoves.Length - 1];
                isBlack = !lastMove.IsBlack;
            }

            int row = ConsoleIO.GetIntInRange("Enter a row: ", 1, Game.GomokuEngine.WIDTH);
            int col = ConsoleIO.GetIntInRange("Enter a column: ", 1, Game.GomokuEngine.WIDTH);
            return new Game.Stone(row - 1, col - 1, isBlack);
        }
    }
}
