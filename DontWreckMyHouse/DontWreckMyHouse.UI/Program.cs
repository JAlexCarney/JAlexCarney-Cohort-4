using System;
using Ninject;

namespace DontWreckMyHouse.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.Kernel.Get<Controller>();
            controller.Run();
        }
    }
}
