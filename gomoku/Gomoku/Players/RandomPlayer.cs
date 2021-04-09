using Gomoku.Game;
using System;

namespace Gomoku.Players
{
    public class RandomPlayer : IPlayer
    {
        private static string[] titles = {"Dr.", "Professor", "Chief Exec", "Specialist", "The Honorable",
            "Prince", "Princess", "The Venerable", "The Eminent"};

        private static string[] names = {
            "Evelyn", "Wyatan", "Jud", "Danella", "Sarah", "Johnna",
            "Vicki", "Alano", "Trever", "Delphine", "Sigismundo",
            "Shermie", "Filide", "Daniella", "Annmarie", "Bartram",
            "Pennie", "Rafael", "Celine", "Kacey", "Saree", "Tu",
            "Erny", "Evonne", "Charita", "Anny", "Mavra", "Fredek",
            "Silvio", "Cam", "Hulda", "Nanice", "Iolanthe", "Brucie",
            "Kara", "Paco"};

        private static string[] lastNames = {"Itch", "Potato", "Mushroom", "Grape", "Mouse", "Feet",
            "Nerves", "Sweat", "Sweet", "Bug", "Piles", "Trumpet", "Shark", "Grouper", "Flutes", "Showers",
            "Humbug", "Cauliflower", "Shoes", "Hopeless", "Zombie", "Monster", "Fuzzy"};

        private Random random = new Random();
        public string Name { get; private set; }

        public RandomPlayer()
        {
            Name = $"{titles[random.Next(titles.Length)]} {names[random.Next(names.Length)]} {lastNames[random.Next(lastNames.Length)]}";
        }

        public Stone GenerateMove(Stone[] previousMoves)
        {
            bool isBlack = true;
            if(previousMoves != null && previousMoves.Length > 0)
            {
                Stone lastMove = previousMoves[previousMoves.Length - 1];
                isBlack = !lastMove.IsBlack;
            }        

            return new Stone(
                    random.Next(GomokuEngine.WIDTH),
                    random.Next(GomokuEngine.WIDTH),
                    isBlack);
        }
    }
}
