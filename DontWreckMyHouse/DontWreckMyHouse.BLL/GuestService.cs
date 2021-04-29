using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL
{
    public class GuestService
    {
        private IGuestRepository repo;

        public GuestService(IGuestRepository repo)
        {
            this.repo = repo;
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
    }
}
