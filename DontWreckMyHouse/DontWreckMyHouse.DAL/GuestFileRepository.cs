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
    public class GuestFileRepository : IGuestRepository
    {
        private readonly string fileName;
        private List<Guest> guests;
        private readonly ILogger logger;

        public GuestFileRepository(string fileName, ILogger logger)
        {
            this.fileName = fileName;
            this.logger = logger;
        }

        public List<Guest> ReadAll()
        {
            Load();
            return guests;
        }

        public Guest ReadByEmail(string email)
        {
            Load();
            return guests.Where(g => g.Email == email).FirstOrDefault();
        }

        public Guest ReadById(int id) 
        {
            Load();
            return guests.Where(g => g.Id == id).FirstOrDefault();
        }

        private void Load()
        {
            guests = new List<Guest>();
            if (!File.Exists(fileName))
            {
                return;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(fileName);
            }
            catch (IOException ex)
            {
                string message = "Failed to read from guest file";
                logger.Log(message);
                throw new RepositoryException(message, ex);
            }

            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);
                if (guest != null)
                {
                    guests.Add(guest);
                }
            }
        }

        private Guest Deserialize(string[] fields)
        {
            if (fields.Length != 6)
            {
                return null;
            }

            try
            {
                var result = new Guest
                {
                    Id = int.Parse(fields[0]),
                    FirstName = fields[1],
                    LastName = fields[2],
                    Email = fields[3],
                    Phone = fields[4],
                    State = fields[5]
                };
                return result;
            }
            catch (Exception ex)
            {
                string message = "Failed to deserialize guest data";
                logger.Log(message);
                throw new RepositoryException(message, ex);
            }
        }
    }
}
