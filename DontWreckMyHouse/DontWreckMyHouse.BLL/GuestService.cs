using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Loggers;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL
{
    public class GuestService
    {
        private readonly IGuestRepository repo;
        private readonly ILogger logger;

        public GuestService(IGuestRepository repo, ILogger logger)
        {
            this.logger = logger;
            this.repo = repo;
        }

        public Result<List<Guest>> ReadAll()
        {
            var result = new Result<List<Guest>>();
            List<Guest> found = repo.ReadAll();
            if (found != null)
            {
                result.Data = found;
            }
            else
            {
                result.AddMessage("Failed to find guests.");
            }
            return result;
        }

        public Result<Guest> ReadByEmail(string email)
        {
            var result = new Result<Guest>();
            Guest found = repo.ReadByEmail(email);
            if (found != null)
            {
                result.Data = found;
            }
            else
            {
                result.AddMessage("Failed to find guest with that email.");
            }
            return result;
        }

        public Result<Guest> ReadById(int id) 
        {
            var result = new Result<Guest>();
            Guest found = repo.ReadById(id);
            if (found != null)
            {
                result.Data = found;
            }
            else
            {
                result.AddMessage("Failed to find guest with that ID.");
            }
            return result;
        }
    }
}
