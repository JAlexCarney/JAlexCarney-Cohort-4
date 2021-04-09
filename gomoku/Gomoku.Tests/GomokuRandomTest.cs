using Gomoku.Game;
using Gomoku.Players;
using NUnit.Framework;
using System;

namespace Gomoku.Tests
{
    [TestFixture]
    public class GomokuRandomTest
    {
        [Test]
        public void ShouldFinish()
        {
            RandomPlayer one = new RandomPlayer();
            RandomPlayer two = new RandomPlayer();
            GomokuEngine game = new GomokuEngine(one, two);

            // Random behavior can't really be tested.
            // "test" verifies that a game will eventually end with two RandomPlayers.
            while(!game.IsOver)
            {
                Result result;
                do
                {
                    // Get the current (random) player and generate a random
                    // stone from the existing game moves.
                    Stone stone = game.Current.GenerateMove(game.Stones);
                    result = game.Place(stone);
                    Console.WriteLine(result);

                } while(!result.IsSuccess);
            }
        }

        [Test]
        public void MakeNames()
        {
            for(int i = 0; i < 100; i++)
            {
                RandomPlayer player = new RandomPlayer();
                Console.WriteLine(player.Name);
            }
        }
    }
}