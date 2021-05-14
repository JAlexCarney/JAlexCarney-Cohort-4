using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;

namespace FieldAgent.DAL.Repos
{
    class ADOReportsRepository : IReportsRepository
    {
        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            throw new NotImplementedException();
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
