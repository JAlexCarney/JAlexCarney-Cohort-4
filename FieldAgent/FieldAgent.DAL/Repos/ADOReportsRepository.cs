using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using System.Data.SqlClient;

namespace FieldAgent.DAL.Repos
{
    public class ADOReportsRepository : IReportsRepository
    {
        private string _sqlConnectionString;

        public ADOReportsRepository(string sqlConnectionString) 
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            List<ClearanceAuditListItem> data;
            var response = new Response<List<ClearanceAuditListItem>>();
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString)) 
            {
                string sql = @"";
            }
            response.Success = true;
            return response;
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            throw new NotImplementedException();
        }
    }
}
