using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class AgencyAgent
    {
        // Table Properties
        public int AgencyId { get; set; }
        public int AgentId { get; set; }
        public int SecurityClearenceId { get; set; }
        [Required]
        [MaxLength(50)]
        public Guid BadgeId { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public bool IsActive { get; set; }
        
        // Navigation Properties
        public Agency Agency { get; set; }
        public Agent Agent { get; set; }
        public SecurityClearance SecurityClearance { get; set; }
    }
}
