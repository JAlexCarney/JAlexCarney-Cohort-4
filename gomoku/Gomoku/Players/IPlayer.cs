using Gomoku.Game;

namespace Gomoku.Players
{
    public interface IPlayer
    {
        string Name { get; }

        Stone GenerateMove(Stone[] previousMoves);
    }
}
