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
using System.Data;

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
            List<ClearanceAuditListItem> data = new List<ClearanceAuditListItem>();
            var response = new Response<List<ClearanceAuditListItem>>();
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand(@"ClearanceAudit", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AgencyId", agencyId);
                command.Parameters.AddWithValue("@SecurityClearanceId", securityClearanceId);

                try
                {
                    connection.Open();
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new ClearanceAuditListItem
                            {
                                BadgeId = Guid.Parse(dataReader["BadgeId"].ToString()),
                                NameLastFirst = dataReader["NameLastFirst"].ToString(),
                                DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                                ActivationDate = DateTime.Parse(dataReader["ActivationDate"].ToString()),
                                DeactivationDate = dataReader["DeactivationDate"] is DBNull ? null : DateTime.Parse(dataReader["DeactivationDate"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    response.Data = null;
                    response.Success = false;
                    return response;
                }
            }
            response.Data = data;
            response.Success = true;
            return response;
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            List<PensionListItem> data = new List<PensionListItem>();
            var response = new Response<List<PensionListItem>>();
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand(@"Pension", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AgencyId", agencyId);

                try
                {
                    connection.Open();
                    using (var dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            data.Add(new PensionListItem
                            {
                                AgencyName = dataReader["AgencyName"].ToString(),
                                BadgeId = Guid.Parse(dataReader["BadgeId"].ToString()),
                                NameLastFirst = dataReader["NameLastFirst"].ToString(),
                                DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                                DeactivationDate = DateTime.Parse(dataReader["DeactivationDate"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex) 
                {
                    response.Message = ex.Message;
                    response.Data = null;
                    response.Success = false;
                    return response;
                }
            }
            response.Data = data;
            response.Success = true;
            return response;
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            List<TopAgentListItem> data = new List<TopAgentListItem>();
            var response = new Response<List<TopAgentListItem>>();
            using (SqlConnection connection = new SqlConnection(_sqlConnectionString))
            {
                var command = new SqlCommand(@"TopAgents", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                try {
                    connection.Open();
                    using (var dataReader = command.ExecuteReader())
                    {
                        int agents = 0;
                        while (dataReader.Read() && agents < 3)
                        {
                            data.Add(new TopAgentListItem
                            {
                                NameLastFirst = dataReader["NameLastFirst"].ToString(),
                                DateOfBirth = DateTime.Parse(dataReader["DateOfBirth"].ToString()),
                                CompletedMissionCount = int.Parse(dataReader["CompletedMissionCount"].ToString())
                            });
                            agents++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message;
                    response.Data = null;
                    response.Success = false;
                    return response;
                }
            }
            response.Data = data;
            response.Success = true;
            return response;
        }
    }
}
