using System;

namespace SolarFarm.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            SolarFarmController solarFarmController = new ();
            solarFarmController.Run();
        }
    }
}
