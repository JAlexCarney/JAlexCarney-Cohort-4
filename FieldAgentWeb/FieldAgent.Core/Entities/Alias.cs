using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [MaxLength(50)]
        public string AliasName { get; set; }
        [Required]
        public Guid InterpolId { get; set; }
        [MaxLength(50)]
        public string Persona { get; set; }

        // Navigation Properties
        public Agent Agent { get; set; }
    }
}
