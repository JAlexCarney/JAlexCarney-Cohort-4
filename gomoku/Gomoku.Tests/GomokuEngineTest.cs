using Gomoku.Game;
using Gomoku.Players;
using NUnit.Framework;

namespace Gomoku.Tests
{
    [TestFixture]
    class GomokuEngineTest
    {
        private readonly HumanPlayer one = new HumanPlayer("Dori");
        private readonly HumanPlayer two = new HumanPlayer("Nemo");
        private GomokuEngine game;

        [SetUp]
        public void Setup()
        {
            // Create a new game before each test runs.
            game = new GomokuEngine(one, two);
        }

        [Test]
        public void BlackShouldStart()
        {
            // The GomokuEngine randomly selects a player as the current player
            // when an instance is created and tracks whose turn it is after that.
            // Given that, at the beginning of a game, the `IsBlacksTurn` property
            // should always be "true" (black always starts first).
            Assert.IsTrue(game.IsBlacksTurn);
        }

        [Test]
        public void GameShouldNotBeOverAtStart()
        {
            // The GomokuEngine tracks the state of the game including if the game has completed.
            // At the beginning of a game, the `IsOver` property should be false.
            Assert.False(game.IsOver);
        }

        [Test]
        public void WinnerShouldBeNullAtStart()
        {
            // The GomokuEngine tracks the state of the game
            // including who the winner is once the game has completed.
            // At the beginning of a game, the `Winner` property should be null.
            Assert.IsNull(game.Winner);
        }

        [Test]
        public void HumanPlayerShouldReturnNullStone()
        {
            // Because human players are expected to explicitly specify
            // where they'd like to place a stone on the game board
            // attempting to automatically generate a move by calling
            // the `GenerateMove()` method should return null.
            Stone stone = one.GenerateMove(game.Stones);
            Assert.IsNull(stone);
        }


        [Test]
        public void ShouldSwapAfterSuccessfulStonePlacement()
        {
            // The GomokuEngine tracks the state of the game including whose turn it is.
            // Check that the GomokuEngine swaps the current player
            // after a stone has been successfully placed.

            // Get the current player.
            IPlayer previous = game.Current;

            // Rely upon the GomokuEngine's `IsBlacksTurn` property to determine the current player's stone color.
            bool isBlack = game.IsBlacksTurn;

            // Create a new stone for the uppermost left hand spot on the board.
            Stone stone = new Stone(0, 0, isBlack);

            // Place the stone and capture the result into a variable.
            Result result = game.Place(stone);

            // Check that the stone was successfully placed.
            Assert.True(result.IsSuccess);

            // Check that the current player has changed.
            IPlayer next = game.Current;
            Assert.AreNotEqual(previous, next);

            // Check that it's no longer black's turn.
            Assert.False(game.IsBlacksTurn);
        }

        [Test]
        public void ShouldSwapIfTheSwapMethodIsCalledDirectly()
        {
            // Check that the current player is swapped after the `Swap()` method is called.

            // NOTE: Because the `Swap()` method is called internally by the Gomoku class
            // after a player has successfully placed a stone, it's only necessary to call
            // the `Swap()` method directly if you're implementing one of the alternative
            // opening rules (see http://gomokuworld.com/gomoku/2 for more information).

            // Get the current player.
            IPlayer previous = game.Current;

            // Manually swap the current player.
            game.Swap();

            // Get the current player again.
            IPlayer next = game.Current;

            // Check that the previous and next players are not the same player object.
            Assert.AreNotEqual(previous, next);
        }

        [Test]
        public void ShouldNotPlayOffTheBoard()
        {
            // The GomokuEngine's `Place()` method validates the stone placement
            // before it adds a stone to the game board. You can check the Result object
            // returned from the `Place()` method to determine if a stone was successfully placed or not.

            // Rely upon the GomokuEngine class's `IsBlacksTurn` property to determine the current player's stone color.
            bool isBlack = game.IsBlacksTurn;

            // Create a stone whose row is far off of the game board.
            Stone stone = new Stone(55, 4, isBlack);

            // Place the stone and capture the result into a variable.
            Result result = game.Place(stone);

            // Check that the result was unsuccessful.
            Assert.False(result.IsSuccess);
            Assert.AreEqual("Stone is off the board.", result.Message);
        }

        [Test]
        public void ShouldNotPlayOutOfTurn()
        {
            // The GomokuEngine's `Place()` method validates the stone placement
            // before it adds a stone to the game board. You can check the Result object
            // returned from the `Place()` method to determine if a stone was successfully placed or not.

            // NOTE: You should rely upon the GomokuEngine's `IsBlacksTurn` property to
            // determine the current player's stone color. We're explicitly passing true or false
            // for the Stone class constructor's `isBlack` parameter value to check that the
            // `Place()` method will correctly prevent playing the wrong color stone.

            Result result = game.Place(new Stone(5, 5, false)); // invalid move, it's black's turn
            Assert.False(result.IsSuccess);
            Assert.AreEqual("Wrong player.", result.Message);

            result = game.Place(new Stone(5, 5, true)); // valid move
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            result = game.Place(new Stone(6, 6, true)); // invalid move, it's white's turn
            Assert.False(result.IsSuccess);
            Assert.AreEqual("Wrong player.", result.Message);
        }

        [Test]
        public void ShouldNotAllowDuplicateMove()
        {
            // The GomokuEngine's `Place()` method validates the stone placement
            // before it adds a stone to the game board. You can check the Result object
            // returned from the `Place()` method to determine if a stone was successfully placed or not.

            // Rely upon the GomokuEngine's `IsBlacksTurn` property to determine the current player's stone color.
            bool isBlack = game.IsBlacksTurn;

            // Create a new stone for the uppermost left hand spot on the board.
            Stone stone = new Stone(0, 0, isBlack);

            // Place the stone and capture the result into a variable.
            Result result = game.Place(stone);

            // Check that the result was successful.
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Rely upon the GomokuEngine's `IsBlacksTurn` property to determine the current player's stone color.
            isBlack = game.IsBlacksTurn;

            // Create a new stone (again) for the uppermost left hand spot on the board.
            stone = new Stone(0, 0, isBlack);

            // Place the stone and capture the result into a variable.
            result = game.Place(stone);

            // Check that the result was unsuccessful.
            Assert.False(result.IsSuccess);
            Assert.AreEqual("Duplicate move.", result.Message);
        }

        [Test]
        public void BlackShouldWinInFiveMoves()
        {
            // Get a reference to the first player (i.e. the black player).
            IPlayer black = game.Current;

            // Black player's first move.
            Result result = game.Place(new Stone(0, 0, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's first move.
            result = game.Place(new Stone(1, 0, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's second move.
            result = game.Place(new Stone(0, 1, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's second move.
            result = game.Place(new Stone(1, 1, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's third move.
            result = game.Place(new Stone(0, 2, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's third move.
            result = game.Place(new Stone(1, 2, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's fourth move.
            result = game.Place(new Stone(0, 3, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's fourth move.
            result = game.Place(new Stone(1, 3, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's fifth move... the winning move of the game.
            // Not only should the result be successful, but it should contain the expected "winning" message.
            result = game.Place(new Stone(0, 4, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.AreEqual($"{black.Name} wins.", result.Message);

            // Check that the game is in fact over and that the winner was the black player.
            Assert.True(game.IsOver);
            Assert.AreEqual(black, game.Winner);
        }

        [Test]
        public void StoneCountShouldMatch()
        {
            // The GomokuEngine class tracks the state of the game including all of the successfully placed stones.
            // You can retrieve a list of the successfully placed stones
            // by referencing the GomokuEngine `Stones` property.

            // Check that the array of stones doesn't contain any items at the start of the game.
            Assert.AreEqual(0, game.Stones.Length);

            // Successfully place a stone.
            Result result = game.Place(new Stone(0, 0, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Check that the array of stones contains one stone.
            Assert.AreEqual(1, game.Stones.Length);

            // Unsuccessfully place a stone by attempting to play where a stone is already placed.
            result = game.Place(new Stone(0, 0, game.IsBlacksTurn));
            Assert.False(result.IsSuccess);
            Assert.AreEqual("Duplicate move.", result.Message);

            // Check that the array of stones still contains just one stone.
            Assert.AreEqual(1, game.Stones.Length);

            // Successfully place a stone adjacent to the first stone that was placed.
            result = game.Place(new Stone(1, 0, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Check that the array of stones now contains two stones.
            Assert.AreEqual(2, game.Stones.Length);
        }

        [Test]
        public void ShouldEndInDraw()
        {
            int[] rows = { 0, 2, 1, 3, 4, 6, 5, 7, 8, 10, 9, 11, 12, 14, 13 };
            foreach(int row in rows)
            {
                for(int col = 0; col < GomokuEngine.WIDTH; col++)
                {
                    Result result = game.Place(new Stone(row, col, game.IsBlacksTurn));

                    // Every placed stone should be successful.
                    Assert.True(result.IsSuccess);

                    // If the game isn't over...
                    if(!game.IsOver)
                    {
                        // Check that the result has a null message.
                        Assert.IsNull(result.Message);
                    }
                    else
                    {
                        // Check that the result of the last placed stone contains a "draw" message.
                        Assert.AreEqual("Game ends in a draw.", result.Message);
                    }
                }
            }

            // Check that the game is in fact over and that there wasn't a winner.
            Assert.True(game.IsOver);
            Assert.IsNull(game.Winner);
        }

        [Test]
        public void ShouldNotAllowPlayAfterGameHasEnded()
        {
            // Get a reference to the first player (i.e. the black player).
            IPlayer black = game.Current;

            // Black player's first move.
            Result result = game.Place(new Stone(0, 0, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's first move.
            result = game.Place(new Stone(1, 0, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's second move.
            result = game.Place(new Stone(0, 1, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's second move.
            result = game.Place(new Stone(1, 1, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's third move.
            result = game.Place(new Stone(0, 2, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's third move.
            result = game.Place(new Stone(1, 2, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's fourth move.
            result = game.Place(new Stone(0, 3, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // White player's fourth move.
            result = game.Place(new Stone(1, 3, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.IsNull(result.Message);

            // Black player's fifth move... the winning move of the game.
            // Not only should the result be successful, but it should contain the expected "winning" message.
            result = game.Place(new Stone(0, 4, game.IsBlacksTurn));
            Assert.True(result.IsSuccess);
            Assert.AreEqual($"{black.Name} wins.", result.Message);

            // Check that the game is in fact over and that the winner was the black player.
            Assert.True(game.IsOver);
            Assert.AreEqual(black, game.Winner);

            // Attempting to play another stone should fail.
            result = game.Place(new Stone(1, 4, game.IsBlacksTurn));
            Assert.False(result.IsSuccess);
            Assert.AreEqual("Game is over.", result.Message);
        }
    }
}
