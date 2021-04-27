using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Repositories
{
    public interface IReservationRepository
    {
        public Reservation Create(Host host, Reservation reservation);
        public List<Reservation> ReadByHost(Host host);
        public Reservation Delete(Host host, Reservation reservation);
    }
}
