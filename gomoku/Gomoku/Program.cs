namespace Gomoku
{
    class Program
    {
        static void Main(string[] args)
        {
            GomokuIOController gomokuIOController = new GomokuIOController();
            do
            {
                gomokuIOController.StartGame();
                System.Console.WriteLine();
            } while (ConsoleIO.GetYesNo("Play Again [y/n]: "));

            // goodbye!
            System.Console.Clear();
            ConsoleIO.WriteWithColor("Goodbye!\n", System.ConsoleColor.Yellow);
        }
    }
}
