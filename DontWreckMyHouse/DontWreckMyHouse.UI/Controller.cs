using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.BLL;

namespace DontWreckMyHouse.UI
{
    class Controller
    {
        private View view;
        private ReservationService reservationService;
        private HostService hostService;

        public Controller(ReservationService reservationService, HostService hostService, View view) 
        {
            // Assign Service Objects
            this.reservationService = reservationService;
            this.hostService = hostService;
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
            string hostEmail = view.GetEmail("host");
            var hostResult = hostService.ReadHostByEmail(hostEmail);
            if (!hostResult.Success)
            {
                view.DisplayStatus(false, hostResult.Messages);
                // Prompt Continue
                view.EnterToContinue();
                return;
            }

            string successMessage = $"Found Host.";
            view.DisplayStatus(true, successMessage);
            // Get Reservations List
            var reservationsResult = reservationService.ReadByHost(hostResult.Data);
            if (!reservationsResult.Success)
            {
                view.DisplayStatus(false, reservationsResult.Messages);
            }
            else
            {
                successMessage = $"Reservations Found Under Host.";
                view.DisplayStatus(true, successMessage);
                // Display Reservations
                view.DisplayHeader(
                    $"{hostResult.Data.LastName}: {hostResult.Data.City}, {hostResult.Data.State}",
                    false);
                view.DisplayReservations(reservationsResult.Data);
            }

            // Prompt Continue
            view.EnterToContinue();
        }

        private void MakeReservation() 
        {
            // Display Header
            view.DisplayHeader(MainMenuOption.MakeReservation.ToLabel());
            // Get Guest
            // Get Host
            // Display Existing Reservations
            // Get Start Date
            // Get End Date
            // Display Summary
            // Get Confirmation
            // Display Success or Error
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
    }
}
