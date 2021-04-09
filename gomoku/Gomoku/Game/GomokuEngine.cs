using Gomoku.Players;
using System;

namespace Gomoku.Game
{
    public class GomokuEngine
    {
        public const int WIDTH = 15;
        private const char BLACK_SYMBOL = 'X';
        private const char WHITE_SYMBOL = 'O';

        private IPlayer playerOne;
        private IPlayer playerTwo;

        private char[,] board = new char[WIDTH, WIDTH];
        private Stone[] stones = new Stone[WIDTH * WIDTH];
        private int stoneIndex = 0;

        public Stone[] Stones
        {
            get
            {
                Stone[] copy = new Stone[stoneIndex];
                Array.Copy(stones, copy, stoneIndex);
                return copy;
            }
        }

        public bool IsOver { get; private set; }

        public IPlayer Current { get; private set; }

        public IPlayer Winner { get; private set; }

        public bool IsBlacksTurn { get; private set; } = true;

        public GomokuEngine(IPlayer p1, IPlayer p2)
        {
            playerOne = p1;
            playerTwo = p2;
            Current = new Random().NextDouble() < 0.5 ? playerOne : playerTwo;
        }

        public Result Place(Stone stone)
        {
            if(IsOver)
            {
                return new Result("Game is over.");
            }

            if(!IsValid(stone))
            {
                return new Result("Stone is off the board.");
            }

            if(IsBlacksTurn != stone.IsBlack)
            {
                return new Result("Wrong player.");
            }

            if(board[stone.Row, stone.Column] != 0)
            {
                return new Result("Duplicate move.");
            }

            board[stone.Row, stone.Column] = IsBlacksTurn ? BLACK_SYMBOL : WHITE_SYMBOL;
            stones[stoneIndex++] = stone;

            if(IsWin(stone))
            {
                IsOver = true;
                Winner = Current;
                return new Result($"{Current.Name} wins.", true);
            }

            if(stoneIndex == WIDTH * WIDTH)
            {
                IsOver = true;
                return new Result("Game ends in a draw.", true);
            }

            IsBlacksTurn = !IsBlacksTurn;
            Swap();
            return new Result(null, true);
        }

        public void Swap()
        {
            Current = Current == playerOne ? playerTwo : playerOne;
        }

        private bool IsValid(Stone stone)
        {
            return stone != null
                        && stone.Row >= 0
                        && stone.Row < WIDTH
                        && stone.Column >= 0
                        && stone.Column < WIDTH;
        }

        private bool IsWin(Stone stone)
        {
            char symbol = board[stone.Row, stone.Column];
            return IsHorizontalWin(stone.Row, stone.Column, symbol)
                        || IsVerticalWin(stone.Row, stone.Column, symbol)
                        || IsDiagonalDownWin(stone.Row, stone.Column, symbol)
                        || IsDiagonalUpWin(stone.Row, stone.Column, symbol);
        }

        private bool IsHorizontalWin(int row, int column, char symbol)
        {
            return Count(row, column, 1, 0, symbol)
                + Count(row, column, -1, 0, symbol) == 4;
        }

        private bool IsVerticalWin(int row, int column, char symbol)
        {
            return Count(row, column, 0, 1, symbol)
                + Count(row, column, 0, -1, symbol) == 4;
        }

        private bool IsDiagonalDownWin(int row, int column, char symbol)
        {
            return Count(row, column, 1, 1, symbol)
                + Count(row, column, -1, -1, symbol) == 4;
        }

        private bool IsDiagonalUpWin(int row, int column, char symbol)
        {
            return Count(row, column, -1, 1, symbol)
                + Count(row, column, 1, -1, symbol) == 4;
        }

        private int Count(int row, int col, int deltaRow, int deltaCol, char symbol)
        {
            int result = 0;
            int r = row + deltaRow;
            int c = col + deltaCol;
            while(r >= 0 && r < WIDTH
                        && c >= 0 && c < WIDTH
                        && board[r, c] == symbol)
            {
                result++;
                r += deltaRow;
                c += deltaCol;
            }

            return result;
        }
    }
}