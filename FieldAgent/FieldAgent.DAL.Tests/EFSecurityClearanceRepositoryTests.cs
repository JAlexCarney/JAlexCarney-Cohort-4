using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.DAL.Repos;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Tests
{
    class EFSecurityClearanceRepositoryTests
    {
        private EFSecurityClearanceRepository repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            var context = new FieldAgentDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repo = new EFSecurityClearanceRepository(context);
        }

        [Test]
        public void ShouldGet()
        {
            // Arrange
            var expected = MakeSecurityClearance();

            // Act
            var response = repo.Get(expected.SecurityClearanceId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.SecurityClearanceName, response.Data.SecurityClearanceName);
        }

        [Test]
        public void ShouldFailToGetWithUnkownId()
        {
            // Act
            var response = repo.Get(-1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find SecurityClearance with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldGetAll()
        {
            // Act
            var response = repo.GetAll();

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(5, response.Data.Count);
        }

        // Helper Function
        private SecurityClearance MakeSecurityClearance() 
        {
            return new SecurityClearance 
            {
                SecurityClearanceId = 1,
                SecurityClearanceName = "None"
            };
        }
    }
}
