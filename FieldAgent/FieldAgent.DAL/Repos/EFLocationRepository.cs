using System;
using System.Collections.Generic;
using System.Linq;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

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
            Location toRemove;
            var response = new Response();
            try
            {
                toRemove = context.Location.Find(locationId);
                if (toRemove == null)
                {
                    response.Message = "Failed to find Location with given Id.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            context.Location.Remove(toRemove);
            context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Location> Get(int locationId)
        {
            Location found;
            var response = new Response<Location>();
            try 
            {
                 found = context.Location.Find(locationId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
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
            List<Location> aliases;
            var response = new Response<List<Location>>();
            try 
            {
                aliases = context.Location.Where(a => a.AgencyId == agencyId).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Success = true;
            response.Data = aliases;
            return response;
        }

        public Response<Location> Insert(Location location)
        {
            Location inserted;
            var response = new Response<Location>();
            try 
            {
                inserted = context.Location.Add(location).Entity;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Data = inserted;
            response.Success = true;
            return response;
        }

        public Response Update(Location location)
        {
            Location updating;
            var response = new Response();
            try
            {
                updating = context.Location.Find(location.LocationId);
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
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Success = true;
            return response;
        }
    }
}
