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
    public class EFAliasRepository : IAliasRepository
    {
        private FieldAgentDbContext context;

        public EFAliasRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int aliasId)
        {
            var toRemove = context.Alias.Find(aliasId);
            var response = new Response();
            if (toRemove == null)
            {
                response.Message = "Failed to find Agent with given Id.";
                return response;
            }
            context.Alias.Remove(toRemove);
            context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Alias> Get(int aliasId)
        {
            Alias found = context.Alias.Find(aliasId);
            var response = new Response<Alias>();
            if (found == null)
            {
                response.Message = "Failed to find Alias with given Id.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<List<Alias>> GetByAgent(int agentId)
        {
            List<Alias> aliases = context.Alias.Where(a => a.AgentId == agentId).ToList();
            var response = new Response<List<Alias>>();
            response.Success = true;
            response.Data = aliases;
            return response;
        }

        public Response<Alias> Insert(Alias alias)
        {
            Alias inserted = context.Alias.Add(alias).Entity;
            var response = new Response<Alias>();
            response.Data = inserted;
            response.Success = true;
            context.SaveChanges();
            return response;
        }

        public Response Update(Alias alias)
        {
            Alias updating = context.Alias.Find(alias.AliasId);
            var response = new Response();
            if (updating == null)
            {
                response.Message = "Failed to find Alias with given Id.";
                return response;
            }
            updating.AliasName = alias.AliasName;
            updating.AgentId = alias.AgentId;
            updating.InterpolId = alias.InterpolId;
            updating.Persona = alias.Persona;
            context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
