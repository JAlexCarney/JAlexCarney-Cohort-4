using SustainableForaging.BLL;
using SustainableForaging.DAL;
using System;
using System.IO;
using Ninject;
using SustainableForaging.Core.Models;

namespace SustainableForaging.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string forageFileDirectory = Path.Combine(projectDirectory, "data", "forage_data");
            string foragerFilePath = Path.Combine(projectDirectory, "data", "foragers.csv");
            string itemFilePath = Path.Combine(projectDirectory, "data", "items.txt");

            NinjectContainer.Configure(ApplicationMode.Live, forageFileDirectory, foragerFilePath, itemFilePath);
            Controller controller = NinjectContainer.Kernel.Get<Controller>();
            controller.Run();
        }

    }
}
