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
    public class EFSecurityClearanceRepository : ISecurityClearanceRepository
    {
        private FieldAgentDbContext context;

        public EFSecurityClearanceRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            SecurityClearance found = context.SecurityClearance.Find(securityClearanceId);
            var response = new Response<SecurityClearance>();
            if (found == null)
            {
                response.Message = "Failed to find SecurityClearance with given Id.";
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }

        public Response<List<SecurityClearance>> GetAll()
        {
            List<SecurityClearance> found = context.SecurityClearance.ToList();
            var response = new Response<List<SecurityClearance>>();
            response.Success = true;
            response.Data = found;
            return response;
        }
    }
}
