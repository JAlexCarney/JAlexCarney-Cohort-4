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
    public class EFAgentRepository : IAgentRepository
    {
        private FieldAgentDbContext context;

        public EFAgentRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int agentId)
        {
            var toRemove = context.Agent.Find(agentId);
            var response = new Response();
            if (toRemove == null)
            {
                response.Message = "Failed to find Agent with given Id.";
                return response;
            }
            var aliasesToRemove = context.Alias.Where(a => a.AgentId == agentId);
            if (aliasesToRemove.Any()) 
            {
                foreach (Alias alias in aliasesToRemove) 
                {
                    context.Alias.Remove(alias);
                    context.SaveChanges();
                }
            }
            var agencyAgentsToRemobe = context.AgencyAgent.Where(a => a.AgentId == agentId);
            if (agencyAgentsToRemobe.Any())
            {
                foreach (AgencyAgent agencyAgent in agencyAgentsToRemobe)
                {
                    context.AgencyAgent.Remove(agencyAgent);
                    context.SaveChanges();
                }
            }
            var missionAgentsToRemove = context.MissionAgent.Where(a => a.AgentId == agentId);
            if (missionAgentsToRemove.Any())
            {
                foreach (MissionAgent missionAgent in missionAgentsToRemove)
                {
                    context.MissionAgent.Remove(missionAgent);
                    context.SaveChanges();
                }
            }
            context.Agent.Remove(toRemove);
            context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Agent> Get(int agentId)
        {
            Agent found = context.Agent.Find(agentId);
            //Agent found = context.Agent.Where(a => a.AgentId == agentId).FirstOrDefault();
            var response = new Response<Agent>();
            if (found == null) 
            {
                response.Message = "Failed to find Agent with given Id.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<List<Mission>> GetMissions(int agentId)
        {
            Agent found = context.Agent
                .Include(a => a.Missions)
                .Where(a => a.AgentId == agentId)
                .FirstOrDefault();
            var response = new Response<List<Mission>>();
            if (found == null) 
            {
                response.Message = "Failed to find Agent with given Id.";
                return response;
            }
            if (found.Missions == null)
            {
                response.Data = new List<Mission>();
            }
            else 
            {
                response.Data = found.Missions.ToList();
            }
            response.Success = true;
            return response;
        }

        public Response<Agent> Insert(Agent agent)
        {
            Agent inserted = context.Agent.Add(agent).Entity;
            var response = new Response<Agent>();
            response.Data = inserted;
            response.Success = true;
            context.SaveChanges();
            return response;
        }

        public Response Update(Agent agent)
        {
            Agent updating = context.Agent.Find(agent.AgentId);
            var response = new Response();
            if (updating == null)
            {
                response.Message = "Failed to find Agent with given Id.";
                return response;
            }
            updating.FirstName = agent.FirstName;
            updating.LastName = agent.LastName;
            updating.DateOfBirth = agent.DateOfBirth;
            updating.Height = agent.Height;
            context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
