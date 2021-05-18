using NUnit.Framework;
using System;
using FieldAgent.DAL.Repos;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Tests
{
    class EFAgentRepositoryTests
    {
        private EFAgentRepository repo;
        private EFAliasRepository aliasRepo;

        [SetUp]
        public void Setup() 
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            var context = new FieldAgentDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repo = new EFAgentRepository(context);
            aliasRepo = new EFAliasRepository(context);
        }

        [Test]
        public void ShouldInsert()
        {
            // Arrange
            var data = MakeAgent();

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
            var expected = MakeAgent();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.Get(inserted.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.FirstName, response.Data.FirstName);
        }

        [Test]
        public void ShouldFailToGetWithUnkownId() 
        {
            // Act
            var response = repo.Get(-1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find Agent with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldGetEmptyMissionsList() 
        {
            // Arrange
            var expected = MakeAgent();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.GetMissions(inserted.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(0, response.Data.Count);
        }

        [Test]
        public void ShouldFailToGetMissionsListFromUnknownId()
        {
            // Act
            var response = repo.GetMissions(-1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find Agent with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldUpdate() 
        {
            // Arrange
            var data = MakeAgent();
            var expected = repo.Insert(data).Data;
            expected.FirstName = "Jeff";
            expected.LastName = "Anderson";
            expected.DateOfBirth = new DateTime(1990, 12, 16);
            expected.Height = 6M;

            // Act
            var response = repo.Update(expected);
            var actual = repo.Get(expected.AgentId).Data;

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
        }

        [Test]
        public void ShouldDelete() 
        {
            // Arrange
            var data = MakeAgent();
            var toRemove = repo.Insert(data).Data;

            // Act
            var response = repo.Delete(toRemove.AgentId);
            var response2 = repo.Get(toRemove.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.IsFalse(response2.Success);
            Assert.AreEqual("Failed to find Agent with given Id.", response2.Message);
        }

        [Test]
        public void ShouldCascadeDelete() 
        {
            var data = MakeAgent();
            var data2 = MakeAlias();

            var response1 = repo.Insert(data);
            var response2 = aliasRepo.Insert(data2);
            var response3 = repo.Delete(response1.Data.AgentId);
            var response4 = repo.Get(response1.Data.AgentId);
            var response5 = aliasRepo.Get(response2.Data.AliasId);

            Assert.IsTrue(response1.Success);
            Assert.IsTrue(response2.Success);
            Assert.IsTrue(response3.Success);
            Assert.IsFalse(response4.Success);
            Assert.IsFalse(response5.Success);
        }

        // Helper Functions
        private static Agent MakeAgent() 
        {
            return new Agent
            {
                FirstName = "Alex",
                LastName = "Carney",
                DateOfBirth = new DateTime(1997, 12, 16),
                Height = 6.5M
            };
        }

        private static Alias MakeAlias()
        {
            return new Alias
            {
                AgentId = 1,
                AliasName = "Zander",
                InterpolId = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695"),
                Persona = "Cool"
            };
        }
    }
}
