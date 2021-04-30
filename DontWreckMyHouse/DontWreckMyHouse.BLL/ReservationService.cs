using System;
using System.Collections.Generic;
using System.Linq;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Loggers;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IHostRepository hostRepository;
        private readonly IGuestRepository guestRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly ILogger logger;

        public ReservationService(IHostRepository hostRepository,
            IGuestRepository guestRepository,
            IReservationRepository reservationRepository,
            ILogger logger) 
        {
            this.hostRepository = hostRepository;
            this.guestRepository = guestRepository;
            this.reservationRepository = reservationRepository;
            this.logger = logger;
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

        private static bool DateIsAfterOrEqual(DateTime one, DateTime two) 
        {
            return one.Subtract(two).Ticks >= 0;
        }

        private static bool MissingRequiredFields(Reservation reservation) 
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
            }
            else if (host == null)
            {
                result.AddMessage("Must provide a host.");
            }
            else if (!hostRepository.ReadAll().Contains(host))
            {
                result.AddMessage("Host is not in database.");
            }
            else if (MissingRequiredFields(reservation)) 
            {
                result.AddMessage("Reservation is missing required fields.");
            }
            else if (guestRepository.ReadById(reservation.GuestId) == null) 
            {
                result.AddMessage("Guest data could not be found.");
            }
            else if (DateIsAfterOrEqual(reservation.StartDate, reservation.EndDate)) 
            {
                result.AddMessage("End date must be after start date.");
            }
            else if (OverlapingDates(host, reservation)) 
            {
                result.AddMessage("Date ranges can not overlap.");
            }

            if (!result.Success) 
            {
                logger.Log($"Failed to create reservation becuase => {result.Messages[0]}");
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

        public Result<Reservation> Delete(Host host, Reservation reservation) 
        {
            var reservations = reservationRepository.ReadByHost(host);
            var result = new Result<Reservation>();
            if (reservations == null) 
            {
                result.AddMessage("That host has no reservations.");
            }
            else if (!reservations.Contains(reservation)) 
            {
                result.AddMessage("That reservation does not exist with that host.");
            }
            else if (reservation.StartDate.Subtract(DateTime.Now).Ticks < 0)
            {
                result.AddMessage("Can not delete a past reservation.");
            }

            if (!result.Success) 
            {
                logger.Log($"Failed to delete reservation becuase => {result.Messages[0]}");
                return result;
            }

            reservationRepository.Delete(host, reservation);
            result.Data = reservation;
            return result;
        }

        public Result<Reservation> Update(Host host, Reservation oldReservation, Reservation newReservation)
        {
            var updateResult = new Result<Reservation>();
            if (oldReservation.Id != newReservation.Id
                || oldReservation.GuestId != newReservation.GuestId)
            {
                updateResult.AddMessage("Cannot update Id or Guest");
                return updateResult;
            }
            var deletionResult = Delete(host, oldReservation);
            if (!deletionResult.Success)
            {
                return deletionResult;
            }
            var creationResult = Create(host, newReservation);
            if (!creationResult.Success) 
            {
                Create(host, deletionResult.Data);
                return creationResult;
            }
            updateResult.Data = newReservation;
            return updateResult;
        }
    }
}
