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
    public class EFMissionRepository : IMissionRepository
    {
        private FieldAgentDbContext context;

        public EFMissionRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response Delete(int missionId)
        {
            var toRemove = context.Mission.Find(missionId);
            var response = new Response();
            if (toRemove == null)
            {
                response.Message = "Failed to find mission with given Id.";
                return response;
            }
            context.Mission.Remove(toRemove);
            context.SaveChanges();
            response.Success = true;
            return response;
        }

        public Response<Mission> Get(int missionId)
        {
            Mission found = context.Mission.Find(missionId);
            var response = new Response<Mission>();
            if (found == null)
            {
                response.Message = "Failed to find mission with given Id.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            List<Mission> missions = context.Mission.Where(a => a.AgencyId == agencyId).ToList();
            var response = new Response<List<Mission>>();
            response.Success = true;
            response.Data = missions;
            return response;
        }

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            List<Mission> missions = context.Mission
                .Include(m => m.Agents)
                .Where(m => m.Agents.Any(a => a.AgentId == agentId))
                .ToList();
            var response = new Response<List<Mission>>();
            response.Success = true;
            response.Data = missions;
            return response;
        }

        public Response<Mission> Insert(Mission mission)
        {
            Mission inserted = context.Mission.Add(mission).Entity;
            var response = new Response<Mission>();
            response.Data = inserted;
            response.Success = true;
            context.SaveChanges();
            return response;
        }

        public Response Update(Mission mission)
        {
            Mission updating = context.Mission.Find(mission.MissionId);
            var response = new Response();
            if (updating == null)
            {
                response.Message = "Failed to find mission with given Id.";
                return response;
            }
            updating.CodeName = mission.CodeName;
            updating.StartDate = mission.StartDate;
            updating.ProjectedEndDate = mission.ProjectedEndDate;
            updating.ActualEndDate = mission.ActualEndDate;
            updating.OperationalCost = mission.OperationalCost;
            updating.Notes = mission.Notes;
            context.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
