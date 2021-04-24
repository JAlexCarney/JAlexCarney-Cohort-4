using NUnit.Framework;
using SustainableForaging.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SustainableForaging.DAL.Tests
{
    public class ForagerFileRepositoryTest
    {
        [SetUp]
        public void Setup() 
        {
            File.Copy(@"data\foragers-seed.csv", @"data\foragers.csv", true);
        }

        [Test]
        public void ShouldFindAll()
        {
            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");
            List<Forager> all = repo.FindAll();
            Assert.AreEqual(1000, all.Count);
        }

        [Test]
        public void ShouldAdd()
        {
            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");

            Forager expected = MakeAlexCarney();

            Forager actual = repo.Add(expected);

            Assert.AreEqual(36, actual.Id.Length);
            Assert.IsTrue(repo.FindAll().Contains(actual));
        }

        [Test]
        public void ShouldReadAfterAdd() 
        {
            ForagerFileRepository repo = new ForagerFileRepository(@"data\foragers.csv");

            Forager expected = MakeAlexCarney();

            repo.Add(expected);
            Forager actual = repo.FindAll().Where(f => f.LastName == "Carney").FirstOrDefault();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(1001, repo.FindAll().Count);
        }

        public Forager MakeAlexCarney() 
        {
            Forager forager = new Forager
            {
                FirstName = "Alex",
                LastName = "Carney",
                State = "CA"
            };
            return forager;
        }

    }
}
