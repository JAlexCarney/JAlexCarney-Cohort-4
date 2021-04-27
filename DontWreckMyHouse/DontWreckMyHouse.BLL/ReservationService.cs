using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Repositories;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private IHostRepository hostRepository;
        private IGuestRepository guestRepository;
        private IReservationRepository reservationRepository;

        public ReservationService(IHostRepository hostRepository, IGuestRepository guestRepository, IReservationRepository reservationRepository) 
        {
            this.hostRepository = hostRepository;
            this.guestRepository = guestRepository;
            this.reservationRepository = reservationRepository;
        }

        public Result<Reservation> Create(Reservation reservation) 
        {
            throw new NotImplementedException();
        }

        public Result<List<Reservation>> ReadByHost(Host host) 
        {
            var result = new Result<List<Reservation>>();
            var found = reservationRepository.ReadByHost(host);
            if (found != null && found.Count > 0)
            {
                result.Data = found;
            }
            else 
            {
                result.AddMessage("Failed to find any reservations for that host.");
            }
            return result;
        }

        public Result<Reservation> ReadById(Host host, int id) 
        {
            throw new NotImplementedException();
        }

        public Result<Reservation> Update(Reservation oldReservation, Reservation newReservation) 
        {
            throw new NotImplementedException();
        }
    }
}
