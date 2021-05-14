using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class Mission
    {
        // Table Properties
        public int MissionId { get; set; }
        public int AgencyId { get; set; }
        public string CodeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal OperationalCost { get; set; }
        public string Notes { get; set; }

        // Navigation Properties
        public List<MissionAgent> MissionAgents { get; set; }
    }
}
