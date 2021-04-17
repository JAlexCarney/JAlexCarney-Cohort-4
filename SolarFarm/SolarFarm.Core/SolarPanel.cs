using System;

namespace SolarFarm.Core
{
    public class SolarPanel
    {
        public string Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public DateTime YearInstalled { get; set; }
        public SolarPanelMaterial Material { get; set; }
        public bool IsTracking { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SolarPanel panel &&
                   Section == panel.Section &&
                   Row == panel.Row &&
                   Column == panel.Column &&
                   YearInstalled == panel.YearInstalled &&
                   Material == panel.Material &&
                   IsTracking == panel.IsTracking;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Section, Row, Column, YearInstalled, Material, IsTracking);
        }
    }
}
