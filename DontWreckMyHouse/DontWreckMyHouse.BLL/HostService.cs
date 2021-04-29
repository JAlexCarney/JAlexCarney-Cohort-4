using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Models;
using System.Collections.Generic;

namespace DontWreckMyHouse.BLL
{
    public class HostService
    {
        private IHostRepository repo;

        public HostService(IHostRepository repo) 
        {
            this.repo = repo;
        }

        public Result<List<Host>> ReadAll() 
        {
            var result = new Result<List<Host>>();
            List<Host> found = repo.ReadAll();
            if (found != null)
            {
                result.Data = found;
            }
            else
            {
                result.AddMessage("Failed to find hosts.");
            }
            return result;
        }

        public Result<Host> ReadByEmail(string email)
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
