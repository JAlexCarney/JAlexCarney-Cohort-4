using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Repositories;
using System.IO;

namespace DontWreckMyHouse.DAL
{
    public class ReservationFileRepository : IReservationRepository
    {
        readonly string directoryName;
        Dictionary<string, List<Reservation>> reservations;

        public ReservationFileRepository(string directoryName) 
        {
            this.directoryName = directoryName;
            Load();
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

        private void Save()
        {

        }

        private void Load() 
        {
            reservations = new Dictionary<string, List<Reservation>>();
            foreach (string fileName in Directory.EnumerateFiles(directoryName, "*.csv"))
            {
                string[] lines;
                try
                {
                    lines = File.ReadAllLines(fileName);
                }
                catch (IOException ex)
                {
                    //TODO: Create Custom Exeption
                    throw new Exception("Failed to read from file.", ex);
                }
                var reservationsInFile = new List<Reservation>();
                for (int i = 1; i < lines.Length; i++) // skip the header
                {
                    string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                    Reservation reservation = Deserialize(fields);
                    if (reservation != null)
                    {
                        reservationsInFile.Add(reservation);
                    }
                }
                reservations.Add(fileName.Substring(directoryName.Length+1, fileName.Length - directoryName.Length - 5), reservationsInFile);
            }
        }

        private Reservation Deserialize(string[] fields)
        {
            if (fields.Length != 5)
            {
                return null;
            }

            // TODO: Add try catch here
            var result = new Reservation
            {
                Id = int.Parse(fields[0]),
                StartDate = DateTime.Parse(fields[1]),
                EndDate = DateTime.Parse(fields[2]),
                GuestId = int.Parse(fields[3]),
                Total = decimal.Parse(fields[4])
            };
            return result;
        }
    }
}
