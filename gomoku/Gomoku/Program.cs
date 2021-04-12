namespace Gomoku
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a game controller
            GomokuController gomokuController = new GomokuController();
            do
            {
                // start playing!
                gomokuController.StartGame();
                System.Console.WriteLine();
            } while (ConsoleIO.GetYesNo("Play Again [y/n]: "));

            // goodbye!
            System.Console.Clear();
            ConsoleIO.WriteWithColor("Goodbye!\n", System.ConsoleColor.Yellow);
        }
    }
}
