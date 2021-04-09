namespace Gomoku.Game
{
    public class Stone
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public bool IsBlack { get; private set; }

        public Stone(int row, int column, bool isBlack)
        {
            Row = row;
            Column = column;
            IsBlack = isBlack;
        }
    }
}