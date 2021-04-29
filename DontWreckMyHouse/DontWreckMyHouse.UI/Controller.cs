using System.Collections.Generic;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.BLL;
using System;

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
            view.DisplayReservations(reservationList);
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
            view.DisplayReservations(reservationsList);
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
            // Get Guest
            // Get Host
            // Display Reservations
            // Select Reservation By ID
            // Display Header
            // Get new Start Date or Defualt
            // Get new End Date or Default
            // Display Summary
            // Get Confirmation
            // Display Success or Error
            // Prompt Continue
            view.EnterToContinue();
        }

        private void CancelReservation() 
        {
            // Display Header
            view.DisplayHeader(MainMenuOption.CancelReservation.ToLabel());
            // Get Guest
            // Get Host
            // Display existing reservations
            // Select reservation by Id
            // Display Success or Error
            view.EnterToContinue();
        }

        // Helper Functions
        private Host GetHost() 
        {
            string hostEmail = view.GetEmail("host");
            var hostResult = hostService.ReadByEmail(hostEmail);
            if (!hostResult.Success)
            {
                view.DisplayStatusShort(false, hostResult.Messages);
                // Prompt Continue
                view.EnterToContinue();
                return null;
            }

            string successMessage = $"Found Host.";
            view.DisplayStatusShort(true, successMessage);
            return hostResult.Data;
        }

        private Guest GetGuest()
        {
            string guestEmail = view.GetEmail("guest");
            var guestResult = guestService.ReadByEmail(guestEmail);
            if (!guestResult.Success)
            {
                view.DisplayStatusShort(false, guestResult.Messages);
                // Prompt Continue
                view.EnterToContinue();
                return null;
            }

            string successMessage = $"Found Guest.";
            view.DisplayStatusShort(true, successMessage);
            return guestResult.Data;
        }

        private List<Reservation> GetReservationsList(Host host) 
        {
            var reservationsResult = reservationService.ReadByHost(host);
            if (!reservationsResult.Success)
            {
                view.DisplayStatusShort(false, reservationsResult.Messages);
                return null;
            }
            string successMessage = $"Reservations Found Under Host.";
            view.DisplayStatusShort(true, successMessage);
            return reservationsResult.Data;
        }

        private decimal CalculateTotal(Host host, Reservation reservation) 
        {
            decimal total = 0M;
            for(DateTime date = reservation.StartDate; date != reservation.EndDate; date = date.AddDays(1)) 
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
