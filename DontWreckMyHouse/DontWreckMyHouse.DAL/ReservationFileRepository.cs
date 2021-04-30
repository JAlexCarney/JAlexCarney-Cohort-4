using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Loggers;
using DontWreckMyHouse.Core.Exceptions;
using System.IO;

namespace DontWreckMyHouse.DAL
{
    public class ReservationFileRepository : IReservationRepository
    {
        private readonly string directoryName;
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private Dictionary<string, List<Reservation>> reservations;
        private Dictionary<string, int> nextIds;
        private readonly ILogger logger;

        public ReservationFileRepository(string directoryName, ILogger logger) 
        {
            this.directoryName = directoryName;
            this.logger = logger;
        }

        public Reservation Create(Host host, Reservation reservation)
        {
            Load();
            if (reservations.ContainsKey(host.Id))
            {
                reservation.Id = nextIds[host.Id];
                nextIds[host.Id]++;
                reservations[host.Id].Add(reservation);
            }
            else 
            {
                if (!nextIds.ContainsKey(host.Id))
                {
                    nextIds.Add(host.Id, 1);
                }
                else 
                {
                    nextIds[host.Id] = 1;
                }
                reservation.Id = nextIds[host.Id];
                var newList = new List<Reservation>
                {
                    reservation
                };
                reservations.Add(host.Id, newList);
            }
            Save(host.Id);
            return reservation;
        }

        public Reservation Delete(Host host, Reservation reservation)
        {
            Load();
            reservations[host.Id].Remove(reservation);
            if (reservations[host.Id].Count == 0)
            {
                string filePath = Path.Combine(directoryName, $"{host.Id}.csv");
                File.Delete(filePath);
                reservations.Remove(host.Id);
            }
            else
            {
                Save(host.Id);
            }
            return reservation;
        }

        public List<Reservation> ReadByHost(Host host)
        {
            Load();
            if (reservations.ContainsKey(host.Id))
            {
                return reservations[host.Id];
            }
            return null;
        }

        private void Save(string key)
        {
            string filePath = Path.Combine(directoryName, $"{key}.csv");
            try
            {
                File.WriteAllText(filePath, "");
                using var writer = new StreamWriter(filePath);
                writer.WriteLine(HEADER);

                if (reservations[key] == null)
                {
                    return;
                }

                foreach (var reservation in reservations[key])
                {
                    writer.WriteLine(Serialize(reservation));
                }
            }
            catch (IOException ex)
            {
                string message = "could not write reservations";
                logger.Log(message);
                throw new Exception(message, ex);
            }
        }

        private void Load() 
        {
            reservations = new Dictionary<string, List<Reservation>>();
            nextIds = new Dictionary<string, int>();
            foreach (string fileName in Directory.EnumerateFiles(directoryName, "*.csv"))
            {
                string[] lines;
                try
                {
                    lines = File.ReadAllLines(fileName);
                }
                catch (IOException ex)
                {
                    string message = "Failed to read from reservations file.";
                    logger.Log(message);
                    throw new RepositoryException(message, ex);
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
                string hostId = fileName.Substring(directoryName.Length + 1, fileName.Length - directoryName.Length - 5);
                nextIds.Add(hostId, reservationsInFile[^1].Id+1);
                reservations.Add(hostId, reservationsInFile);
            }
        }

        private static string Serialize(Reservation reservation) 
        {
            return string.Format("{0},{1},{2},{3},{4}",
                reservation.Id,
                $"{reservation.StartDate.Year:0000}-{reservation.StartDate.Month:00}-{reservation.StartDate.Day:00}",
                $"{reservation.EndDate.Year:0000}-{reservation.EndDate.Month:00}-{reservation.EndDate.Day:00}",
                reservation.GuestId,
                reservation.Total);
        }

        private Reservation Deserialize(string[] fields)
        {
            if (fields.Length != 5)
            {
                return null;
            }

            try
            {
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
            catch (Exception ex) 
            {
                string message = "Failed to deserialize Reservation data";
                logger.Log(message);
                throw new RepositoryException(message, ex);
            }
        }
    }
}
