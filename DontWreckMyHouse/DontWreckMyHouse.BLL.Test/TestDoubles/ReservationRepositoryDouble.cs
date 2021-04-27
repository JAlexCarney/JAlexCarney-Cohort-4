using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Test.TestDoubles
{
    public class ReservationRepositoryDouble : IReservationRepository
    {
        public static readonly Reservation RESERVATION = new Reservation
        {
            Id = 1,
            StartDate = new DateTime(2022, 2, 11),
            EndDate = new DateTime(2022, 2, 13),
            GuestId = 1,
            Total = 1022.50M
        };
        private Dictionary<string, List<Reservation>> reservations = new Dictionary<string, List<Reservation>>();

        public ReservationRepositoryDouble()
        {
            var value = new List<Reservation>();
            value.Add(RESERVATION);
            reservations.Add(HostRepositoryDouble.HOST.Id, value);
        }

        public Reservation Create(Host host, Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation Delete(Host host, Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> ReadByHost(Host host)
        {
            if (reservations.ContainsKey(host.Id))
            {
                return reservations[host.Id];
            }
            return null;
        }
    }
}
