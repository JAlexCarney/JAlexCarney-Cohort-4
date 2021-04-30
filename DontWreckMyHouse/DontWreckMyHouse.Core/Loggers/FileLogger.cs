using System;
using System.IO;
using DontWreckMyHouse.Core.Exceptions;

namespace DontWreckMyHouse.Core.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly string filename;
        public FileLogger(string filename) 
        {
            this.filename = filename;
        }
        
        public void Log(string message) 
        {
            try
            {
                using StreamWriter sw = File.AppendText(filename);
                sw.WriteLine($"{DateTime.Now:R}: {message}");
            }
            catch (IOException ex) 
            {
                throw new LoggerException("Failed to write to log", ex);
            }
        }
    }
}
