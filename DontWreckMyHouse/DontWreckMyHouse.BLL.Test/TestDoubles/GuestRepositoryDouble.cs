using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Test.TestDoubles
{
    class GuestRepositoryDouble : IGuestRepository
    {
        public static readonly Guest GUEST = new Guest
        {
            Id = 1,
            FirstName = "Sullivan",
            LastName = "Lomas",
            Email = "user@website.com",
            Phone = "(702) 7768761",
            State = "NV"
        };
        private List<Guest> guests = new List<Guest>();

        public GuestRepositoryDouble()
        {
            guests.Add(GUEST);
        }

        public List<Guest> ReadAll()
        {
            return guests;
        }

        public Guest ReadByEmail(string email)
        {
            return guests.Where(g => g.Email == email).FirstOrDefault();
        }
    }
}
