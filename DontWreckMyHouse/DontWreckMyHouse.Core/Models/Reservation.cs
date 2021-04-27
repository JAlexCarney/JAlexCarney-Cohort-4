using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestId { get; set; }
        public decimal Total { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Reservation reservation &&
                   Id == reservation.Id &&
                   StartDate == reservation.StartDate &&
                   EndDate == reservation.EndDate &&
                   GuestId == reservation.GuestId &&
                   Total == reservation.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, StartDate, EndDate, GuestId, Total);
        }
    }
}
