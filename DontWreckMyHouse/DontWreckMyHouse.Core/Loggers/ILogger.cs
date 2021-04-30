using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Loggers
{
    public interface ILogger
    {
        public void Log(string message);
    }
}
