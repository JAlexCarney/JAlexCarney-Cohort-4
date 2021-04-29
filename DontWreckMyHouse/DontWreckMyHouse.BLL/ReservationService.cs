using System;
using System.Collections.Generic;
using System.Linq;
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

        private bool OverlapingDates(Host host, Reservation toAdd) 
        {
            List<Reservation> reservations = reservationRepository.ReadByHost(host);
            if (reservations == null || reservations.Count == 0) 
            {
                return false;
            }
            return reservations.Any(existing =>
                // overlap exists if start date is within existing reservation
                (DateIsAfterOrEqual(toAdd.StartDate, existing.StartDate) && DateIsAfterOrEqual(existing.EndDate, toAdd.StartDate))
                // overlap exists if end date is within existing reservation
                || (DateIsAfterOrEqual(existing.EndDate, toAdd.EndDate) && DateIsAfterOrEqual(toAdd.EndDate, existing.StartDate))
                // overlap exists if new reservation date range constains old reservation date range
                || (DateIsAfterOrEqual(existing.StartDate, toAdd.StartDate) && DateIsAfterOrEqual(toAdd.EndDate, existing.EndDate))
            );
        }

        private bool DateIsAfterOrEqual(DateTime one, DateTime two) 
        {
            return one.Subtract(two).Ticks >= 0;
        }

        private bool MissingRequiredFields(Reservation reservation) 
        {
            return (reservation.Total <= 0.0M 
                || reservation.GuestId <= 0
                || reservation.StartDate == new DateTime()
                || reservation.EndDate == new DateTime()
            );
        }

        public Result<Reservation> Create(Host host, Reservation reservation) 
        {
            var result = new Result<Reservation>();

            if (reservation == null) 
            {
                result.AddMessage("Must provide a reservation");
                return result;
            }
            if (host == null)
            {
                result.AddMessage("Must provide a host.");
                return result;
            }
            if (!hostRepository.ReadAll().Contains(host))
            {
                result.AddMessage("Host is not in database.");
                return result;
            }
            if (MissingRequiredFields(reservation)) 
            {
                result.AddMessage("Reservation is missing required fields.");
                return result;
            }
            if (DateIsAfterOrEqual(reservation.StartDate, reservation.EndDate)) 
            {
                result.AddMessage("End date must be after start date.");
                return result;
            }
            if (OverlapingDates(host, reservation)) 
            {
                result.AddMessage("Date ranges can not overlap.");
                return result;
            }

            result.Data = reservationRepository.Create(host, reservation);
            return result;
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
