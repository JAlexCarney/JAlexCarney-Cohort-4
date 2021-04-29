using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Test.TestDoubles
{
    public class HostRepositoryDouble : IHostRepository
    {
        public static readonly Host HOST = new Host
        {
            Id = "8597c189-2352-49a2-ba9f-eb400d8dadbf",
            LastName = "Folkerd",
            Email = "user@website.com",
            Phone = "(281) 1808157",
            Address = "59778 Clove Road",
            City = "Houston",
            State = "TX",
            PostalCode = 77075,
            StandardRate = 285M,
            WeekendRate = 356.25M
        };
        private List<Host> hosts = new List<Host>();

        public HostRepositoryDouble()
        {
            hosts.Add(HOST);
            hosts.Add(new Host
            {
                Id = "8597c189-2352-49a2-ba9f-eb400d8dadbf",
                LastName = "Folkerd",
                Email = "user@website.com",
                Phone = "(281) 1808157",
                Address = "59778 Clove Road",
                City = "Houston",
                State = "TX",
                PostalCode = 77075,
                StandardRate = 285M,
                WeekendRate = 356.25M
            });
        }

        public List<Host> ReadAll()
        {
            return hosts;
        }

        public Host ReadByEmail(string email)
        {
            return hosts.Where(h => h.Email == email).FirstOrDefault();
        }
    }
}
