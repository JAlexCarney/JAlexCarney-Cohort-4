using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class SecurityClearance
    {
        // Table Properties
        public int SecurityClearanceId { get; set; }
        [Required]
        [MaxLength(50)]
        public string SecurityClearanceName { get; set; }

        // Navigation Properties
        public List<AgencyAgent> AgencyAgents { get; set; }
    }
}
