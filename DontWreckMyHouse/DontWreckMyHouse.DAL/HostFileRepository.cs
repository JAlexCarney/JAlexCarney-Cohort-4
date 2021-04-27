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
    public class HostFileRepository : IHostRepository
    {
        private readonly string fileName;
        private List<Host> hosts;
        
        public HostFileRepository(string fileName) 
        {
            this.fileName = fileName;
            Load();
        }

        public List<Host> ReadAll()
        {
            return hosts;
        }

        public Host ReadByEmail(string email)
        {
            return hosts.Where(h => h.Email == email).FirstOrDefault();
        }

        private void Load() 
        {
            hosts = new List<Host>();
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
                Host host = Deserialize(fields);
                if (host != null)
                {
                    hosts.Add(host);
                }
            }
        }

        private Host Deserialize(string[] fields) 
        {
            if (fields.Length != 10) 
            {
                return null;
            }

            // TODO: Add try catch here
            var result = new Host
            {
                Id = fields[0],
                LastName = fields[1],
                Email = fields[2],
                Phone = fields[3],
                Address = fields[4],
                City = fields[5],
                State = fields[6],
                PostalCode = int.Parse(fields[7]),
                StandardRate = decimal.Parse(fields[8]),
                WeekendRate = decimal.Parse(fields[9])
            };
            return result;
        }
    }
}
