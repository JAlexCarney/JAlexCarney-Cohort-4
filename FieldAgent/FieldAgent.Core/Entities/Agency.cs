using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class Agency
    {
        // Table Properties
        public int AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        // Navigation Properties
        public List<AgencyAgent> AgencyAgents { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Location> Locations { get; set; }
    }
}
