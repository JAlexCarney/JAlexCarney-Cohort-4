using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL
{
    public class HostService
    {
        private IHostRepository repo;

        public HostService(IHostRepository repo) 
        {
            this.repo = repo;
        }

        public Result<Host> ReadHostByEmail(string email)
        {
            var result = new Result<Host>();
            Host found = repo.ReadByEmail(email);
            if (found != null)
            {
                result.Data = found;
            }
            else
            {
                result.AddMessage("Failed to find host with that email.");
            }
            return result;
        }
    }
}
