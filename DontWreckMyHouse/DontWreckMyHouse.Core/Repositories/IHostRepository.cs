using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Repositories
{
    public interface IHostRepository
    {
        public List<Host> ReadAll();
        public Host ReadByEmail(string email);
    }
}
