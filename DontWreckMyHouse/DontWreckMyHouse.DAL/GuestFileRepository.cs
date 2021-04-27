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
    public class GuestFileRepository : IGuestRepository
    {
        private string fileName;
        private List<Guest> guests;

        public GuestFileRepository(string fileName)
        {
            this.fileName = fileName;
            Load();
        }

        public List<Guest> ReadAll()
        {
            return guests;
        }

        public Guest ReadByEmail(string email)
        {
            return guests.Where(g => g.Email == email).FirstOrDefault();
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
                //TODO: Create Custom Exeption
                throw new Exception("Failed to read from file.", ex);
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

            // TODO: Add try catch here
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
    }
}
