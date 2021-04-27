using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SustainableForaging.BLL;
using NUnit.Framework;
using SustainableForaging.BLL.Tests.TestDoubles;
using SustainableForaging.Core.Models;

namespace SustainableForaging.BLL.Tests
{
    class ForagerServiceTest
    {
        ForagerService service;

        [Test]
        public void ShouldAddValidForager() 
        {
            // Arrange
            service = new ForagerService(new ForagerRepositoryDouble());
            Forager validForager = MakeForager();

            // Act
            Result<Forager> result = service.Add(validForager);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(validForager, result.Value);
        }

        [Test]
        public void ShouldFailToAddDuplicate() 
        {
            // Arrange
            service = new ForagerService(new ForagerRepositoryDouble());
            Forager validForager = MakeForager();

            // Act
            service.Add(validForager);
            Result<Forager> result = service.Add(CopyForager(validForager));

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(null, result.Value);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [Test]
        public void ShouldFailToAddForagerWithMissingData() 
        {
            // Arrange
            service = new ForagerService(new ForagerRepositoryDouble());
            Forager invalidForager = MakeForager();
            invalidForager.FirstName = "";

            // Act
            Result<Forager> result = service.Add(invalidForager);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(null, result.Value);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [Test]
        public void ShouldFailToAddForagerWithInvalidData()
        {
            // Arrange
            service = new ForagerService(new ForagerRepositoryDouble());
            Forager invalidForager = MakeForager();
            invalidForager.State = "Apple";

            // Act
            Result<Forager> result = service.Add(invalidForager);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(null, result.Value);
            Assert.AreEqual(1, result.Messages.Count);
            // Check that it was the expected message
        }

        private static Forager MakeForager()
        {
            Forager forager = new Forager();
            forager.Id = "0e4707f4-407e-4ec9-9665-baca0aaee88c";
            forager.FirstName = "Sally";
            forager.LastName = "Sisse";
            forager.State = "GA";
            return forager;
        }

        private static Forager CopyForager(Forager toCopy) 
        {
            Forager forager = new Forager();
            forager.Id = toCopy.Id;
            forager.FirstName = toCopy.FirstName;
            forager.LastName = toCopy.LastName;
            forager.State = toCopy.State;
            return forager;
        }

    }
}
