using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Guest guest &&
                   Id == guest.Id &&
                   FirstName == guest.FirstName &&
                   LastName == guest.LastName &&
                   Email == guest.Email &&
                   Phone == guest.Phone &&
                   State == guest.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email, Phone, State);
        }
    }
}
