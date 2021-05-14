using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Repos
{
    class EFLocationRepository : ILocationRepository
    {
        public Response Delete(int locationId)
        {
            throw new NotImplementedException();
        }

        public Response<Location> Get(int locationId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<Location> Insert(Location location)
        {
            throw new NotImplementedException();
        }

        public Response Update(Location location)
        {
            throw new NotImplementedException();
        }
    }
}
