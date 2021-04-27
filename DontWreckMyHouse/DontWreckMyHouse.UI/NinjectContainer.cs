using Ninject;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.DAL;

namespace DontWreckMyHouse.UI
{
    class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }

        public static void Configure() 
        {
            Kernel = new StandardKernel();

            Kernel.Bind<IGuestRepository>().To<GuestFileRepository>().WithConstructorArgument("guests.csv");
            Kernel.Bind<IHostRepository>().To<HostFileRepository>().WithConstructorArgument("hosts.csv");
            Kernel.Bind<IReservationRepository>().To<ReservationFileRepository>().WithConstructorArgument("reservations");
        }
    }
}
