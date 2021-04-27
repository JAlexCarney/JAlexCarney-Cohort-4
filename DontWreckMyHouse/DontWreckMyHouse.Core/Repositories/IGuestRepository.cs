using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Repositories
{
    public interface IGuestRepository
    {
        public List<Guest> ReadAll();
        public Guest ReadByEmail(string email);
    }
}
