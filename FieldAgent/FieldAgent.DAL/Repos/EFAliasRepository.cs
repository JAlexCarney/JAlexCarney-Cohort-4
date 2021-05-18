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
            Alias toRemove;
            var response = new Response();
            try 
            {
                toRemove = context.Alias.Find(aliasId);
                if (toRemove == null)
                {
                    response.Message = "Failed to find Agent with given Id.";
                    return response;
                }
                context.Alias.Remove(toRemove);
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

        public Response<Alias> Get(int aliasId)
        {
            var response = new Response<Alias>();
            Alias found;
            try
            {
                found = context.Alias.Find(aliasId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
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
            List<Alias> aliases;
            var response = new Response<List<Alias>>();
            try
            {
                aliases = context.Alias.Where(a => a.AgentId == agentId).ToList();
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

        public Response<Alias> Insert(Alias alias)
        {
            Alias inserted;
            var response = new Response<Alias>();
            try 
            {
                inserted = context.Alias.Add(alias).Entity;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Success = true;
            response.Data = inserted;
            return response;
        }

        public Response Update(Alias alias)
        {
            Alias updating;
            var response = new Response();
            try
            {
                updating = context.Alias.Find(alias.AliasId);
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
