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
    public class EFAgencyAgentRepository : IAgencyAgentRepository
    {
        private FieldAgentDbContext context;

        public EFAgencyAgentRepository() 
        {
            context = FieldAgentDbContext.GetDbContext();
        }

        public EFAgencyAgentRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int agencyid, int agentid)
        {
            var response = new Response();
            try
            {
                var toRemove = context.AgencyAgent
                    .Where(aa => aa.AgencyId == agencyid && aa.AgentId == agentid)
                    .SingleOrDefault();
                if (toRemove == null)
                {
                    response.Message = "Failed to find Agency with given Id.";
                    return response;
                }
                context.AgencyAgent.Remove(toRemove);
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

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            var response = new Response<AgencyAgent>();
            AgencyAgent found;
            try
            {
                found = context.AgencyAgent
                    .Where(aa => aa.AgencyId == agencyid && aa.AgentId == agentid)
                    .SingleOrDefault();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            if (found == null)
            {
                response.Message = "Failed to find AgencyAgent with given Id.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            var response = new Response<List<AgencyAgent>>();
            List<AgencyAgent> agencyAgents;
            try 
            {
                agencyAgents = context.AgencyAgent.Where(a => a.AgencyId == agencyId).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            
            response.Success = true;
            response.Data = agencyAgents;
            return response;
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            var response = new Response<List<AgencyAgent>>();
            List<AgencyAgent> agencyAgents;
            try
            {
                agencyAgents = context.AgencyAgent.Where(a => a.AgentId == agentId).ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Success = true;
            response.Data = agencyAgents;
            return response;
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            var response = new Response<AgencyAgent>();
            AgencyAgent inserted;
            context.SaveChanges();
            try 
            {
                inserted = context.AgencyAgent.Add(agencyAgent).Entity;
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

        public Response Update(AgencyAgent agencyAgent)
        {
            var response = new Response();
            try
            {
                AgencyAgent updating = context.AgencyAgent
                    .Where(aa => aa.AgencyId == agencyAgent.AgencyId && aa.AgencyId == agencyAgent.AgentId)
                    .SingleOrDefault();
                if (updating == null)
                {
                    response.Message = "Failed to find AgencyAgent with given Id.";
                    return response;
                }
                updating.SecurityClearenceId = agencyAgent.SecurityClearenceId;
                updating.BadgeId = agencyAgent.BadgeId;
                updating.ActivationDate = agencyAgent.ActivationDate;
                updating.DeactivationDate = agencyAgent.DeactivationDate;
                updating.IsActive = agencyAgent.IsActive;
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
