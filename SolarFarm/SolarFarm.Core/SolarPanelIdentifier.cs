using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core
{
    public struct SolarPanelIdentifier
    {
        public SolarPanelIdentifier(string section, int row, int column)
        {
            Section = section;
            Row = row;
            Column = column;
        }

        public string Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
