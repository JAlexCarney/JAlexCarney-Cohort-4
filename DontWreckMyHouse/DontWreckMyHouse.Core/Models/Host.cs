using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Host
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public decimal StandardRate { get; set; }
        public decimal WeekendRate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Host host &&
                   Id == host.Id &&
                   LastName == host.LastName &&
                   Email == host.Email &&
                   Phone == host.Phone &&
                   Address == host.Address &&
                   City == host.City &&
                   State == host.State &&
                   PostalCode == host.PostalCode &&
                   StandardRate == host.StandardRate &&
                   WeekendRate == host.WeekendRate;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(LastName);
            hash.Add(Email);
            hash.Add(Phone);
            hash.Add(Address);
            hash.Add(City);
            hash.Add(State);
            hash.Add(PostalCode);
            hash.Add(StandardRate);
            hash.Add(WeekendRate);
            return hash.ToHashCode();
        }
    }
}
