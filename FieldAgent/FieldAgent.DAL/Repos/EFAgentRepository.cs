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
    class EFAgentRepository : IAgentRepository
    {
        public Response Delete(int agentId)
        {
            throw new NotImplementedException();
        }

        public Response<Agent> Get(int agentId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Mission>> GetMissions(int agentId)
        {
            throw new NotImplementedException();
        }

        public Response<Agent> Insert(Agent agent)
        {
            throw new NotImplementedException();
        }

        public Response Update(Agent agent)
        {
            throw new NotImplementedException();
        }
    }
}
