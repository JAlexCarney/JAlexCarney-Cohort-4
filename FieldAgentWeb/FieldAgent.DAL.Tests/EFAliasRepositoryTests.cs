using NUnit.Framework;
using System;
using FieldAgent.DAL.Repos;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Tests
{
    class EFAliasRepositoryTests
    {
        private EFAliasRepository repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            var context = new FieldAgentDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repo = new EFAliasRepository(context);
        }

        [Test]
        public void ShouldInsert()
        {
            // Arrange
            var data = MakeAlias();

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
            var expected = MakeAlias();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.Get(inserted.AliasId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.AliasName, response.Data.AliasName);
        }

        [Test]
        public void ShouldFailToGetWithUnkownId()
        {
            // Act
            var response = repo.Get(-1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find Alias with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldGetByAgentId() 
        {
            // Arrange
            var expected = MakeAlias();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.GetByAgent(inserted.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count);
        }

        [Test]
        public void ShouldUpdate()
        {
            // Arrange
            var data = MakeAlias();
            var expected = repo.Insert(data).Data;
            expected.AliasName = "Jeff";

            // Act
            var response = repo.Update(expected);
            var actual = repo.Get(expected.AliasId).Data;

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.AliasName, actual.AliasName);
        }

        [Test]
        public void ShouldDelete()
        {
            // Arrange
            var data = MakeAlias();
            var toRemove = repo.Insert(data).Data;

            // Act
            var response = repo.Delete(toRemove.AgentId);
            var response2 = repo.Get(toRemove.AgentId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.IsFalse(response2.Success);
            Assert.AreEqual("Failed to find Alias with given Id.", response2.Message);
        }

        // Helper Functions
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
