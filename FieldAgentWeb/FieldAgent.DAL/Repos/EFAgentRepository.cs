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

        public EFAgentRepository()
        {
            context = FieldAgentDbContext.GetDbContext();
        }

        public EFAgentRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int agentId)
        {
            Agent toRemove;
            var response = new Response();
            try
            {
                toRemove = context.Agent.Find(agentId);
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
                context.Agent.Remove(toRemove);
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

        public Response<List<Agent>> GetAll()
        {
            var response = new Response<List<Agent>>();
            List<Agent> found;
            try
            {
                found = context.Agent.ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            if (found == null)
            {
                response.Message = "Failed to find any Agents.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<Agent> Get(int agentId)
        {
            var response = new Response<Agent>();
            Agent found;
            try
            {
                found = context.Agent.Find(agentId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
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
            var response = new Response<List<Mission>>();
            Agent found;
            try
            {
                found = context.Agent
                    .Include(a => a.Missions)
                    .Where(a => a.AgentId == agentId)
                    .FirstOrDefault();
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
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Success = true;
            return response;
        }

        public Response<Agent> Insert(Agent agent)
        {
            Agent inserted;
            var response = new Response<Agent>();
            try 
            {
               inserted = context.Agent.Add(agent).Entity;
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

        public Response Update(Agent agent)
        {
            Agent updating;
            var response = new Response();
            try 
            {
                updating = context.Agent.Find(agent.AgentId);
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
