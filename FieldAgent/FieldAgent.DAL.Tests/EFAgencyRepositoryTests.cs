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
    class EFAgencyRepositoryTests
    {
        private EFAgencyRepository repo;
        private EFLocationRepository locationRepo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseInMemoryDatabase("testDatabase")
                .Options;
            var context = new FieldAgentDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            repo = new EFAgencyRepository(context);
            locationRepo = new EFLocationRepository(context);
        }

        [Test]
        public void ShouldInsert()
        {
            // Arrange
            var data = MakeAgency();

            // Act
            var response = repo.Insert(data);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreNotEqual(0, response.Data.AgencyId);
        }

        [Test]
        public void ShouldGet()
        {
            // Arrange
            var expected = MakeAgency();

            // Act
            var inserted = repo.Insert(expected).Data;
            var response = repo.Get(inserted.AgencyId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.ShortName, response.Data.ShortName);
        }

        [Test]
        public void ShouldFailToGetWithUnkownId()
        {
            // Act
            var response = repo.Get(-1);

            // Assert
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Failed to find Agency with given Id.", response.Message);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldGetAll() 
        {
            // Arrange
            var expected = MakeAgency();

            // Act
            repo.Insert(expected);
            var response = repo.GetAll();

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count);
        }

        [Test]
        public void ShouldUpdate()
        {
            // Arrange
            var data = MakeAgency();
            var expected = repo.Insert(data).Data;
            expected.ShortName = "NSA";
            expected.LongName = "National Security Agency";

            // Act
            var response = repo.Update(expected);
            var actual = repo.Get(expected.AgencyId).Data;

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(expected.ShortName, actual.ShortName);
            Assert.AreEqual(expected.LongName, actual.LongName);
        }

        [Test]
        public void ShouldDelete()
        {
            // Arrange
            var data = MakeAgency();
            var toRemove = repo.Insert(data).Data;

            // Act
            var response = repo.Delete(toRemove.AgencyId);
            var response2 = repo.Get(toRemove.AgencyId);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.IsFalse(response2.Success);
            Assert.AreEqual("Failed to find Agency with given Id.", response2.Message);
        }

        [Test]
        public void ShouldCascadeDelete()
        {
            var data = MakeAgency();
            var data2 = MakeLocation();

            var response1 = repo.Insert(data);
            var response2 = locationRepo.Insert(data2);
            var response3 = repo.Delete(response1.Data.AgencyId);
            var response4 = repo.Get(response1.Data.AgencyId);
            var response5 = locationRepo.Get(response2.Data.LocationId);

            Assert.IsTrue(response1.Success);
            Assert.IsTrue(response2.Success);
            Assert.IsTrue(response3.Success);
            Assert.IsFalse(response4.Success);
            Assert.IsFalse(response5.Success);
        }

        // Helper Functions
        private static Agency MakeAgency()
        {
            return new Agency
            {
               ShortName = "FBI",
               LongName = "Federal Bureau of Investigation"
            };
        }

        private static Location MakeLocation()
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
