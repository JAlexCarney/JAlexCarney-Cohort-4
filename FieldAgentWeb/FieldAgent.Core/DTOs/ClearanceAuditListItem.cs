using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.DTOs
{
    public class ClearanceAuditListItem
    {
        public Guid BadgeId { get; set; }
        public string NameLastFirst { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
    }
}
