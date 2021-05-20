using System;
using System.Collections.Generic;
using System.Linq;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repos
{
    public class EFSecurityClearanceRepository : ISecurityClearanceRepository
    {
        private FieldAgentDbContext context;

        public EFSecurityClearanceRepository()
        {
            context = FieldAgentDbContext.GetDbContext();
        }

        public EFSecurityClearanceRepository(FieldAgentDbContext context) 
        {
            this.context = context;
        }

        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            SecurityClearance found;
            var response = new Response<SecurityClearance>();
            try 
            {
                found = context.SecurityClearance.Find(securityClearanceId);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
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
            List<SecurityClearance> found;
            var response = new Response<List<SecurityClearance>>();
            try 
            {
                found = context.SecurityClearance.ToList();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
            response.Success = true;
            response.Data = found;
            return response;
        }
    }
}
