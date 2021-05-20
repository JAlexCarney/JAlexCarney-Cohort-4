using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class Alias
    {
        // Table Properties
        public int AliasId {get; set;}
        public int AgentId { get; set; }
        public string AliasName { get; set; }
        public Guid InterpolId { get; set; }
        public string Persona { get; set; }

        // Navigation Properties
        public Agent Agent { get; set; }
    }
}
