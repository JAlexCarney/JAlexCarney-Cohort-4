using System.Collections.Generic;
using FieldAgent.Core.Entities;

namespace FieldAgent.Core.Interfaces.DAL
{
    interface ISecurityClearanceRepository
    {
        Response<SecurityClearance> Get(int securityClearanceId);
        Response<List<SecurityClearance>> GetAll();
    }
}
