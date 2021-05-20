using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class MissionAgent
    {
        // Table Properties
        public int MissionId { get; set; }
        public int AgentId { get; set; }

        // Navigation Properties
        public Mission Mission { get; set; }
        public Agent Agent { get; set; }
    }
}
