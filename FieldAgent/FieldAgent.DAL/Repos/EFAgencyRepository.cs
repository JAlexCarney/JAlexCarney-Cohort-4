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
    public class EFAgencyRepository : IAgencyRepository
    {
        private FieldAgentDbContext context;

        public EFAgencyRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int agencyId)
        {
            var toRemove = context.Agency.Find(agencyId);
            var response = new Response();
            if (toRemove == null)
            {
                response.Message = "Failed to find Agency with given Id.";
                return response;
            }
            context.Agency.Remove(toRemove);
            context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Agency> Get(int agencyId)
        {
            Agency found = context.Agency.Find(agencyId);
            var response = new Response<Agency>();
            if (found == null)
            {
                response.Message = "Failed to find Agency with given Id.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<List<Agency>> GetAll()
        {
            List<Agency> found = context.Agency.ToList();
            var response = new Response<List<Agency>>();
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<Agency> Insert(Agency agency)
        {
            Agency inserted = context.Agency.Add(agency).Entity;
            var response = new Response<Agency>();
            response.Data = inserted;
            response.Success = true;
            context.SaveChanges();
            return response;
        }

        public Response Update(Agency agency)
        {
            Agency updating = context.Agency.Find(agency.AgencyId);
            var response = new Response();
            if (updating == null)
            {
                response.Message = "Failed to find Agency with given Id.";
                return response;
            }
            updating.ShortName = agency.ShortName;
            updating.LongName = agency.LongName;
            context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
