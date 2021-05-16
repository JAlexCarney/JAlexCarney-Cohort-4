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
    class EFAgencyAgentRepositoryTests
    {
        private EFAgencyAgentRepository repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            var context = new FieldAgentDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repo = new EFAgencyAgentRepository(context);
        }

        [Test]
        public void ShouldInsert()
        {
            // Arrange
            var data = MakeAgencyAgent();

            // Act
            var response = repo.Insert(data);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreNotEqual(0, response.Data.AgentId);
        }

        [Test]
        public void ShouldGet()
        {
            // Arrange
            var expected = MakeAgencyAgent();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.Get(inserted.AgencyId, inserted.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.BadgeId, response.Data.BadgeId);
        }

        [Test]
        public void ShouldFailToGetWithUnkownId()
        {
            // Act
            var response = repo.Get(-1, -1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find AgencyAgent with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldGetByAgentId()
        {
            // Arrange
            var expected = MakeAgencyAgent();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.GetByAgent(inserted.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count);
        }

        [Test]
        public void ShouldGetByAgencyId()
        {
            // Arrange
            var expected = MakeAgencyAgent();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.GetByAgency(inserted.AgencyId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count);
        }

        [Test]
        public void ShouldUpdate()
        {
            // Arrange
            var data = MakeAgencyAgent();
            var expected = repo.Insert(data).Data;
            expected.BadgeId = new Guid();

            // Act
            var response = repo.Update(expected);
            var actual = repo.Get(expected.AgencyId, expected.AgentId).Data;

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.BadgeId, actual.BadgeId);
        }

        [Test]
        public void ShouldDelete()
        {
            // Arrange
            var data = MakeAgencyAgent();
            var toRemove = repo.Insert(data).Data;

            // Act
            var response = repo.Delete(toRemove.AgencyId, toRemove.AgentId);
            var response2 = repo.Get(toRemove.AgencyId, toRemove.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.IsFalse(response2.Success);
            Assert.AreEqual("Failed to find AgencyAgent with given Id.", response2.Message);
        }

        // Helper Functions
        private Agent MakeAgent()
        {
            return new Agent
            {
                FirstName = "Alex",
                LastName = "Carney",
                DateOfBirth = new DateTime(1997, 12, 16),
                Height = 6.5M
            };
        }

        private Agency MakeAgency()
        {
            return new Agency
            {
                ShortName = "FBI",
                LongName = "Federal Bureau of Investigation"
            };
        }

        private AgencyAgent MakeAgencyAgent() 
        {
            return new AgencyAgent
            {
                AgencyId = 1,
                AgentId = 1,
                BadgeId = new Guid(),
                SecurityClearenceId = 1,
                ActivationDate = new DateTime(1997,12,16),
                DeactivationDate = null,
                IsActive = true
            };
        }
    }
}
