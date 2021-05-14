using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<MissionAgent> MissionAgents { get; set; }
        public List<Alias> Aliases { get; set; }
    }
}
