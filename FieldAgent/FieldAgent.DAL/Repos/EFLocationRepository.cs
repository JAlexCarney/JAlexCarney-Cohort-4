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
    public class EFLocationRepository : ILocationRepository
    {
        private FieldAgentDbContext context;

        public EFLocationRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int locationId)
        {
            var toRemove = context.Location.Find(locationId);
            var response = new Response();
            if (toRemove == null)
            {
                response.Message = "Failed to find Location with given Id.";
                return response;
            }
            context.Location.Remove(toRemove);
            context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Location> Get(int locationId)
        {
            Location found = context.Location.Find(locationId);
            var response = new Response<Location>();
            if (found == null)
            {
                response.Message = "Failed to find Location with given Id.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            List<Location> aliases = context.Location.Where(a => a.AgencyId == agencyId).ToList();
            var response = new Response<List<Location>>();
            response.Success = true;
            response.Data = aliases;
            return response;
        }

        public Response<Location> Insert(Location location)
        {
            Location inserted = context.Location.Add(location).Entity;
            var response = new Response<Location>();
            response.Data = inserted;
            response.Success = true;
            context.SaveChanges();
            return response;
        }

        public Response Update(Location location)
        {
            Location updating = context.Location.Find(location.LocationId);
            var response = new Response();
            if (updating == null)
            {
                response.Message = "Failed to find Location with given Id.";
                return response;
            }
            updating.AgencyId = location.AgencyId;
            updating.LocationName = location.LocationName;
            updating.Street1 = location.Street1;
            updating.Street2 = location.Street2;
            updating.City = location.City;
            updating.PostalCode = location.PostalCode;
            updating.CountryCode = location.CountryCode;
            context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
