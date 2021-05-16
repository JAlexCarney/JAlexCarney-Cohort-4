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

        public EFAgencyAgentRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int agencyid, int agentid)
        {
            var toRemove = context.AgencyAgent
                .Where(aa => aa.AgencyId == agencyid && aa.AgentId == agentid)
                .SingleOrDefault();
            var response = new Response();
            if (toRemove == null)
            {
                response.Message = "Failed to find Agency with given Id.";
                return response;
            }
            context.AgencyAgent.Remove(toRemove);
            context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            AgencyAgent found = context.AgencyAgent
                .Where(aa => aa.AgencyId == agencyid && aa.AgentId == agentid)
                .SingleOrDefault();
            var response = new Response<AgencyAgent>();
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
            List<AgencyAgent> agencyAgents = context.AgencyAgent.Where(a => a.AgencyId == agencyId).ToList();
            var response = new Response<List<AgencyAgent>>();
            response.Success = true;
            response.Data = agencyAgents;
            return response;
        }

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            List<AgencyAgent> agencyAgents = context.AgencyAgent.Where(a => a.AgentId == agentId).ToList();
            var response = new Response<List<AgencyAgent>>();
            response.Success = true;
            response.Data = agencyAgents;
            return response;
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            AgencyAgent inserted = context.AgencyAgent.Add(agencyAgent).Entity;
            var response = new Response<AgencyAgent>();
            response.Data = inserted;
            response.Success = true;
            context.SaveChanges();
            return response;
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            AgencyAgent updating = context.AgencyAgent
                .Where(aa => aa.AgencyId == agencyAgent.AgencyId && aa.AgencyId == agencyAgent.AgentId)
                .SingleOrDefault();
            var response = new Response();
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
            response.Success = true;
            return response;
        }
    }
}
