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
    class EFMissionRepositoryTests
    {
        private EFMissionRepository repo;
        private EFAgentRepository agentRepo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            var context = new FieldAgentDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repo = new EFMissionRepository(context);
            agentRepo = new EFAgentRepository(context);
        }

        [Test]
        public void ShouldInsert()
        {
            // Arrange
            var data = MakeMission();

            // Act
            var response = repo.Insert(data);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreNotEqual(0, response.Data.MissionId);
        }

        [Test]
        public void ShouldGet()
        {
            // Arrange
            var expected = MakeMission();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.Get(inserted.MissionId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.CodeName, response.Data.CodeName);
        }

        [Test]
        public void ShouldFailToGetWithUnkownId()
        {
            // Act
            var response = repo.Get(-1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find mission with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldGetByAgencyId()
        {
            // Arrange
            var expected = MakeMission();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.GetByAgency(inserted.AgencyId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count);
        }

        [Test]
        public void ShouldGetByAgentId() 
        {
            // Arrange
            var agent = MakeAgent();
            var expected = MakeMission();

            // Act
            var inserted = agentRepo.Insert(agent).Data;
            repo.Insert(expected);
            var response = repo.GetByAgency(inserted.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count);
        }

        [Test]
        public void ShouldUpdate()
        {
            // Arrange
            var data = MakeMission();
            var expected = repo.Insert(data).Data;
            expected.CodeName = "Jeff";

            // Act
            var response = repo.Update(expected);
            var actual = repo.Get(expected.MissionId).Data;

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.CodeName, actual.CodeName);
        }

        [Test]
        public void ShouldDelete()
        {
            // Arrange
            var data = MakeMission();
            var toRemove = repo.Insert(data).Data;

            // Act
            var response = repo.Delete(toRemove.MissionId);
            var response2 = repo.Get(toRemove.MissionId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.IsFalse(response2.Success);
            Assert.AreEqual("Failed to find mission with given Id.", response2.Message);
        }

        // Helper Functions
        private Mission MakeMission()
        {
            return new Mission
            {
                AgencyId = 1,
                CodeName = "Operation Test Database",
                StartDate = new DateTime(1997, 12, 16),
                ProjectedEndDate = new DateTime(1998, 12, 16),
                ActualEndDate = null,
                OperationalCost = 120.5M,
                Notes = "A Long Mission"
            };
        }

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
    }
}
