using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Loggers
{
    public class NullLogger : ILogger
    {
        public void Log(string message) { }
    }
}
