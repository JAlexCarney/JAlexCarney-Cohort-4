using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) 
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{DateTime.Now:R}: {message}");
            Console.ResetColor();
        }
    }
}
