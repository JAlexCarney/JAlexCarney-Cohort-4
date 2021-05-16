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
    class EFLocationRepositoryTests
    {
        private EFLocationRepository repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            var context = new FieldAgentDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repo = new EFLocationRepository(context);
        }

        [Test]
        public void ShouldInsert()
        {
            // Arrange
            var data = MakeLocation();

            // Act
            var response = repo.Insert(data);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreNotEqual(0, response.Data.LocationId);
        }

        [Test]
        public void ShouldGet()
        {
            // Arrange
            var expected = MakeLocation();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.Get(inserted.LocationId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.LocationName, response.Data.LocationName);
        }

        [Test]
        public void ShouldFailToGetWithUnkownId()
        {
            // Act
            var response = repo.Get(-1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find Location with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldGetByAgency() 
        {
            // Arrange
            var expected = MakeLocation();

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
            var data = MakeLocation();
            var expected = repo.Insert(data).Data;
            expected.LocationName = "New HQ";

            // Act
            var response = repo.Update(expected);
            var actual = repo.Get(expected.LocationId).Data;

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.LocationName, actual.LocationName);
        }

        [Test]
        public void ShouldDelete()
        {
            // Arrange
            var data = MakeLocation();
            var toRemove = repo.Insert(data).Data;

            // Act
            var response = repo.Delete(toRemove.LocationId);
            var response2 = repo.Get(toRemove.LocationId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.IsFalse(response2.Success);
            Assert.AreEqual("Failed to find Location with given Id.", response2.Message);
        }

        // Helper Functions
        private Location MakeLocation()
        {
            return new Location
            {
                AgencyId = 1,
                LocationName = "HeadQuaters",
                Street1 = "1800 Cool Street",
                Street2 = "",
                City = "Indio",
                PostalCode = "92203",
                CountryCode = "US"
            };
        }
    }
}
