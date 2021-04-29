using System.Collections.Generic;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.BLL;
using System;
using System.Linq;

namespace DontWreckMyHouse.UI
{
    class Controller
    {
        private View view;
        private ReservationService reservationService;
        private HostService hostService;
        private GuestService guestService;

        public Controller(ReservationService reservationService, HostService hostService, GuestService guestService, View view) 
        {
            // Assign Service Objects
            this.reservationService = reservationService;
            this.hostService = hostService;
            this.guestService = guestService;
            // Assign View
            this.view = view;
        }

        public void Run() 
        {
            // Enter Menu Loop
            MenuLoop();
            // Say Goodbye
            view.DisplayHeader("Goodbye.");
        }

        private void MenuLoop() 
        {
            // Display Menu
            // Switch on Choice
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch (option)
                {
                    case MainMenuOption.ViewReservationsByHost:
                        ViewReservationsByHost();
                        break;
                    case MainMenuOption.MakeReservation:
                        MakeReservation();
                        break;
                    case MainMenuOption.EditReservation:
                        EditReservation();
                        break;
                    case MainMenuOption.CancelReservation:
                        CancelReservation();
                        break;
                }
            }
            while (option != MainMenuOption.Exit);
        }

        private void ViewReservationsByHost() 
        {
            // Display Header
            view.DisplayHeader(MainMenuOption.ViewReservationsByHost.ToLabel());
            // Get Host
            Host host = GetHost();
            if (host == null) { return; }
            // Get Reservations List
            List<Reservation> reservationList = GetReservationsList(host);
            // Display Reservations
            view.DisplayHeader(
                    $"{host.LastName}: {host.City}, {host.State}",
                    false);
            view.DisplayReservations(reservationList, guestService);
            // Prompt Continue
            view.EnterToContinue();
        }

        private void MakeReservation() 
        {
            // Display Header
            view.DisplayHeader(MainMenuOption.MakeReservation.ToLabel());
            // Get Host
            Host host = GetHost();
            if (host == null) { return; }
            // Get Guest
            Guest guest = GetGuest();
            if (guest == null) { return; }
            // Get Reservations List
            var reservationsList = GetReservationsList(host);
            // Display Reservations
            view.DisplayHeader(
                $"{host.LastName}: {host.City}, {host.State}",
                false);
            view.DisplayReservations(reservationsList, guestService);
            // Make Reservation
            Reservation reservation = view.MakeReservation(guest);
            reservation.Total = CalculateTotal(host, reservation);
            // Display Reservation Summary
            view.DisplayReservationSummary(reservation);
            // Get Confirmation
            if (!view.GetConfirmation()) { return; }
            // Create Reservation
            var result = reservationService.Create(host, reservation);
            // Display Success or Error
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
                view.EnterToContinue();
                return;
            }
            view.DisplayStatus(true, $"Reservation {result.Data.Id} created.");
            // Prompt Continue
            view.EnterToContinue();
        }

        private void EditReservation() 
        {
            // Display Header
            view.DisplayHeader(MainMenuOption.EditReservation.ToLabel());
            // Get Host
            Host host = GetHost();
            if (host == null) { return; }
            // Get Reservations List
            var reservationsList = GetReservationsListNotEmpty(host);
            if (reservationsList == null) { return; }
            // Display Reservations
            view.DisplayHeader(
                $"{host.LastName}: {host.City}, {host.State}",
                false);
            view.DisplayReservations(reservationsList, guestService);
            // Check if there is anything to update
            if (reservationsList.Where(r => r.StartDate.Subtract(DateTime.Now).Ticks >= 0).Count() == 0)
            {
                view.DisplayStatus(false, "There are no future reservations that can be updated.");
                view.EnterToContinue();
                return;
            }
            // Select reservation by Id
            Reservation toUpdate = view.SelectReservationFromList(reservationsList);
            // Display Header
            view.DisplayHeader($"Editing Reservation {toUpdate.Id}");
            // Get new reservation information
            Reservation updated = view.GetUpdatedReservation(toUpdate);
            updated.Total = CalculateTotal(host, updated);
            // Display Reservation Summary
            view.DisplayReservationSummary(updated);
            // Get Confirmation
            if (!view.GetConfirmation()) { return; }
            // Update reservation
            var result = reservationService.Update(host, toUpdate, updated);
            // Display Success or Error
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
                view.EnterToContinue();
                return;
            }
            view.DisplayStatus(true, $"Reservation {result.Data.Id} updated.");
            // Prompt Continue
            view.EnterToContinue();
        }

        private void CancelReservation() 
        {
            // Display Header
            view.DisplayHeader(MainMenuOption.CancelReservation.ToLabel());
            // Get Host
            Host host = GetHost();
            if (host == null) { return; }
            // Get Reservations List
            var reservationsList = GetReservationsListNotEmpty(host);
            if (reservationsList == null) { return; }
            // Display Reservations
            view.DisplayHeader(
                $"{host.LastName}: {host.City}, {host.State}",
                false);
            view.DisplayReservations(reservationsList, guestService);
            // Check if there is anything to delete
            if (reservationsList.Where(r => r.StartDate.Subtract(DateTime.Now).Ticks >= 0).Count() == 0) 
            {
                view.DisplayStatus(false, "There are no future reservations that can be canceled.");
                view.EnterToContinue();
                return;
            }
            // Select reservation by Id
            Reservation toCancel = view.SelectReservationFromList(reservationsList);
            // Delete Selected Reservation
            var result = reservationService.Delete(host, toCancel);
            // Display Success or Error
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
                view.EnterToContinue();
                return;
            }
            view.DisplayStatus(true, $"Reservation {result.Data.Id} canceled.");
            // Prompt Continue
            view.EnterToContinue();
        }

        // Helper Functions
        private Host GetHost() 
        {
            string hostEmailPrefix = view.GetStartOfEmail("Host");
            var hostsResult = hostService.ReadAll();
            if (!hostsResult.Success) 
            {
                view.DisplayStatus(false, hostsResult.Messages);
                // Prompt Continue
                view.EnterToContinue();
                return null;
            }
            var hosts = hostsResult.Data;
            if (hosts.Any(h => h.Email == hostEmailPrefix))
            {
                var hostResult = hostService.ReadByEmail(hostEmailPrefix);
                if (!hostResult.Success)
                {
                    view.DisplayStatus(false, hostResult.Messages);
                    // Prompt Continue
                    view.EnterToContinue();
                    return null;
                }

                string successMessage = $"Found Host.";
                view.DisplayStatusShort(true, successMessage);
                return hostResult.Data;
            }
            var options = hosts.Where(h => h.Email.StartsWith(hostEmailPrefix)).Take(20).ToList();
            if (options.Count() == 0) 
            {
                view.DisplayStatus(false, $"No hosts found whose emails start with \"{hostEmailPrefix}\"");
                // Prompt Continue
                view.EnterToContinue();
                return null;
            }
            return view.SelectHostFromList(options);
        }

        private Guest GetGuest()
        {
            string guestEmailPrefix = view.GetStartOfEmail("Guest");
            var guestsResult = guestService.ReadAll();
            if (!guestsResult.Success)
            {
                view.DisplayStatus(false, guestsResult.Messages);
                // Prompt Continue
                view.EnterToContinue();
                return null;
            }
            var guests = guestsResult.Data;
            if (guests.Any(h => h.Email == guestEmailPrefix))
            {
                var guestResult = guestService.ReadByEmail(guestEmailPrefix);
                if (!guestResult.Success)
                {
                    view.DisplayStatus(false, guestResult.Messages);
                    // Prompt Continue
                    view.EnterToContinue();
                    return null;
                }

                string successMessage = $"Found Guest.";
                view.DisplayStatusShort(true, successMessage);
                return guestResult.Data;
            }
            var options = guests.Where(h => h.Email.StartsWith(guestEmailPrefix)).Take(20).ToList();
            if (options.Count() == 0)
            {
                view.DisplayStatus(false, $"No guests found whose emails start with \"{guestEmailPrefix}\"");
                // Prompt Continue
                view.EnterToContinue();
                return null;
            }
            return view.SelectGuestFromList(options);
        }

        private List<Reservation> GetReservationsList(Host host) 
        {
            var reservationsResult = reservationService.ReadByHost(host);
            if (!reservationsResult.Success)
            {
                return null;
            }
            string successMessage = $"Reservations Found Under Host.";
            view.DisplayStatusShort(true, successMessage);
            return reservationsResult.Data;
        }

        private List<Reservation> GetReservationsListNotEmpty(Host host)
        {
            var reservationsResult = reservationService.ReadByHost(host);
            if (!reservationsResult.Success)
            {
                view.DisplayStatus(false, reservationsResult.Messages);
                // Prompt Continue
                view.EnterToContinue();
                return null;
            }
            string successMessage = $"Reservations Found Under Host.";
            view.DisplayStatusShort(true, successMessage);
            return reservationsResult.Data;
        }

        private decimal CalculateTotal(Host host, Reservation reservation) 
        {
            decimal total = 0M;
            for(DateTime date = reservation.StartDate; date.Subtract(reservation.EndDate).Ticks < 0; date = date.AddDays(1)) 
            {
                switch (date.DayOfWeek) 
                {
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        total += host.StandardRate;
                        break;
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        total += host.WeekendRate;
                        break;
                }
            }
            return total;
        }
    }
}
