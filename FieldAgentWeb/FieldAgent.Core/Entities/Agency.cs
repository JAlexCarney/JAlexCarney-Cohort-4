using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{
    public class Agency
    {
        // Table Properties
        public int AgencyId { get; set; }
        [Required]
        [MaxLength(50)]
        public string ShortName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LongName { get; set; }

        // Navigation Properties
        public List<AgencyAgent> AgencyAgents { get; set; }
        public List<Mission> Missions { get; set; }
        public List<Location> Locations { get; set; }
    }
}
