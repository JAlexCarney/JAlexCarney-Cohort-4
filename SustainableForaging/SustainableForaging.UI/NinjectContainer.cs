using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using SustainableForaging.DAL;

namespace SustainableForaging.UI
{
    class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }

        public static void Configure(ApplicationMode mode, string file1 = "", string file2 = "", string file3 = "") 
        {
            Kernel = new StandardKernel();

            if (mode == ApplicationMode.Live)
            {
                Kernel.Bind<IForageRepository>().To<ForageFileRepository>().WithConstructorArgument(file1);
                Kernel.Bind<IForagerRepository>().To<ForagerFileRepository>().WithConstructorArgument(file2);
                Kernel.Bind<IItemRepository>().To<ItemFileRepository>().WithConstructorArgument(file3);
            }
            else if (mode == ApplicationMode.Test) 
            {
                // not implemented
            }
        }
    }
}
