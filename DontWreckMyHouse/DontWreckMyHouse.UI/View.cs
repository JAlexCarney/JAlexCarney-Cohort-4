using System;
using System.Collections.Generic;
using DontWreckMyHouse.Core.Models;
using System.Linq;

namespace DontWreckMyHouse.UI
{
    class View
    {
        private ConsoleIO io;

        public View(ConsoleIO io) 
        {
            this.io = io;
        }

        public MainMenuOption SelectMainMenuOption()
        {
            DisplayHeader("Main Menu");
            int min = int.MaxValue;
            int max = int.MinValue;
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            for (int i = 0; i < options.Length; i++)
            {
                MainMenuOption option = options[i];
                io.PrintLineDarkYellow($"{i}. {option.ToLabel()}");
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max - 1}]: ";
            return options[io.ReadInt(message, min, max)];
        }

        public void DisplayHeader(string message, bool clear = true)
        {
            if (clear) { io.Clear(); }
            io.PrintLine("");
            io.PrintLineYellow(message);
            io.PrintLineYellow(new string('=', message.Length));
        }

        public void EnterToContinue()
        {
            io.PrintLine("");
            io.ReadString("Press [Enter] to continue.");
        }

        public void DisplayStatus(bool success, string message)
        {
            DisplayStatus(success, new List<string>() { message });
        }

        public void DisplayStatus(bool success, List<string> messages)
        {
            DisplayHeader(success ? "Success" : "Error");
            foreach (string message in messages)
            {
                if (success)
                {
                    io.PrintLineGreen(message);
                }
                else
                {
                    io.PrintLineRed(message);
                }
            }
        }

        public void DisplayStatusShort(bool success, string message)
        {
            DisplayStatusShort(success, new List<string>() { message });
        }

        public void DisplayStatusShort(bool success, List<string> messages)
        {
            foreach (string message in messages)
            {
                if (success)
                {
                    io.PrintLineGreen("[Success]");
                    io.PrintLineGreen(message);
                }
                else
                {
                    io.PrintLineRed("[Error]");
                    io.PrintLineRed(message);
                }
            }
        }

        public void DisplayReservations(List<Reservation> data)
        {
            if(data == null || data.Count == 0) 
            {
                io.PrintLineDarkYellow("--- Empty ---");
                return;
            }
            var sortedData = data.OrderBy(r => r.StartDate);
            io.PrintLineYellow($"{"ID",3} {"Start Date",10} => {"End Date",10} {"Guest ID",8} {"Total",7}");
            foreach (var value in sortedData)
            {
                //TODO: Replace Guest ID with Guest Email
                io.PrintLineDarkYellow($"{value.Id,3} {value.StartDate,10:d} => {value.EndDate,10:d} {value.GuestId,8} {value.Total,7:C}");
            }
        }

        public void DisplayReservationSummary(Reservation reservation) 
        {
            DisplayHeader("Summary", false);
            io.PrintLineDarkYellow($"Start: {reservation.StartDate:d}");
            io.PrintLineDarkYellow($"End  : {reservation.EndDate:d}");
            io.PrintLineDarkYellow($"Total: {reservation.Total:C}");
        }

        public string GetEmail(string from) 
        {
            return io.ReadString($"Enter {from}'s Email: ");
        }

        public bool GetConfirmation() 
        {
            return io.ReadBool("Is this okay? [y/n]: ");
        }

        public Reservation MakeReservation(Guest guest) 
        {
            Reservation reservation = new Reservation()
            {
                Id = -1,
                StartDate = io.ReadDate("Start (MM/DD/YYYY): "),
                EndDate = io.ReadDate("End (MM/DD/YYYY): "),
                GuestId = guest.Id,
                Total = 0.0M //TODO: Calculate total cost
            };
            return reservation;
        }
    }
}
