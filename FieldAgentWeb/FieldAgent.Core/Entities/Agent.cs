using System;
using System.Collections.Generic;

namespace FieldAgent.Core.Entities
{
    public class Agent
    {
        // Table Properties
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height { get; set; }

        // Navigation Properties
        public List<AgencyAgent> AgencyAgents { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Alias> Aliases { get; set; }
    }
}
