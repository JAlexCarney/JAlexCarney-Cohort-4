using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public enum MainMenuOption
    {
        Exit,
        ViewReservationsByHost,
        MakeReservation,
        EditReservation,
        CancelReservation
    }

    public static class MainMenuOptionExtensions
    {
        public static string ToLabel(this MainMenuOption option)
        {
            switch (option) 
            {
                case MainMenuOption.Exit:
                    return "Exit";
                case MainMenuOption.ViewReservationsByHost:
                    return "View Reservations By Host";
                case MainMenuOption.MakeReservation:
                    return "Make A Reservation";
                case MainMenuOption.EditReservation:
                    return "Edit A Reservation";
                case MainMenuOption.CancelReservation:
                    return "Cancel A Reservation";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
