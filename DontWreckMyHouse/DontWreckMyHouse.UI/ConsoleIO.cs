using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    class ConsoleIO
    {
        private const string INVALID_NUMBER
            = "[INVALID] Enter a valid number.";
        private const string NUMBER_OUT_OF_RANGE
            = "[INVALID] Enter a number between {0} and {1}.";
        private const string REQUIRED
            = "[INVALID] Value is required.";
        private const string INVALID_STATE
            = "[INVALID] Enter a state in Two-Letter format.";
        private const string INVALID_DATE
            = "[INVALID] Enter a valid date format.";
        private const string FUTURE_DATE
            = "[INVALID] Enter a future date.";
        private const string INVALID_BOOL
            = "[INVALID] Please enter 'y' or 'n'.";

        public void Print(string message)
        {
            Console.Write(message);
        }

        public void PrintLine(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintLineRed(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintLine(message);
            Console.ResetColor();
        }

        public void PrintLineGreen(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintLine(message);
            Console.ResetColor();
        }

        public void PrintLineYellow(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintLine(message);
            Console.ResetColor();
        }

        public void PrintLineDarkYellow(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            PrintLine(message);
            Console.ResetColor();
        }

        public string ReadLineCyan()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public string ReadString(string prompt)
        {
            Print(prompt);
            return ReadLineCyan();
        }

        public string ReadRequiredString(string prompt)
        {
            while (true)
            {
                string result = ReadString(prompt);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    return result;
                }
                PrintLineRed(REQUIRED);
            }
        }

        public decimal ReadDecimal(string prompt)
        {
            decimal result;
            while (true)
            {
                if (decimal.TryParse(ReadRequiredString(prompt), out result))
                {
                    return result;
                }

                PrintLineRed(INVALID_NUMBER);
            }
        }

        public decimal ReadDecimal(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                decimal result = ReadDecimal(prompt);
                if (result >= min && result <= max)
                {
                    return result;
                }
                PrintLineRed(string.Format(NUMBER_OUT_OF_RANGE, min, max));
            }
        }

        public int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                if (int.TryParse(ReadRequiredString(prompt), out result))
                {
                    return result;
                }

                PrintLineRed(INVALID_NUMBER);
            }
        }

        public int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                int result = ReadInt(prompt);
                if (result >= min && result <= max)
                {
                    return result;
                }
                PrintLineRed(string.Format(NUMBER_OUT_OF_RANGE, min, max));
            }
        }

        public string ReadTwoLetterStateAbbreviation(string prompt)
        {
            while (true)
            {
                string result = ReadRequiredString(prompt);
                if (result.Length == 2)
                {
                    return result;
                }
                PrintLineRed(INVALID_STATE);
            }
        }

        public bool ReadBool(string prompt)
        {
            while (true)
            {
                string input = ReadRequiredString(prompt).ToLower();
                if (input == "y")
                {
                    return true;
                }
                else if (input == "n")
                {
                    return false;
                }
                PrintLineRed(INVALID_BOOL);
            }
        }

        public DateTime ReadDate(string prompt)
        {
            DateTime result;
            while (true)
            {
                string input = ReadRequiredString(prompt);
                if (DateTime.TryParse(input, out result))
                {
                    return result.Date;
                }
                PrintLineRed(INVALID_DATE);
            }
        }

        public DateTime ReadFutureDate(string prompt)
        {
            DateTime result;
            while (true)
            {
                string input = ReadRequiredString(prompt);
                if (DateTime.TryParse(input, out result))
                {
                    if (result.Subtract(DateTime.Now).Ticks > 0)
                    {
                        return result.Date;
                    }
                    PrintLineRed(FUTURE_DATE);
                }
                else 
                {
                    PrintLineRed(INVALID_DATE);
                }
            }
        }

        internal DateTime ReadDateDefualtable(string prompt, DateTime defualt)
        {
            DateTime result;
            while (true)
            {
                string input = ReadString(prompt);
                if (string.IsNullOrEmpty(input)) 
                {
                    return defualt;
                }
                if (DateTime.TryParse(input, out result))
                {
                    return result.Date;
                }
                PrintLineRed(INVALID_DATE);
            }
        }
    }
}
