using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL.Test
{
    class GuestFileRepositoryTest
    {
        private IGuestRepository repo;

        [SetUp]
        public void SetUp()
        {
            repo = new GuestFileRepository("TestGuests.csv");
        }

        [Test]
        public void ShouldReadAll()
        {
            // Arrange
            var expected = MakeGuest();

            // Act
            var actual = repo.ReadAll();

            // Assert
            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains(expected));
        }

        [Test]
        public void ShouldReadByEmail()
        {
            // Arrange
            var expected = MakeGuest();

            // Act
            var actual = repo.ReadByEmail(expected.Email);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldFailToReadIfEmailNotFound()
        {
            // Act
            var actual = repo.ReadByEmail("test@test.com");

            // Assert
            Assert.IsNull(actual);
        }

        private Guest MakeGuest()
        {
            var guest = new Guest
            {
                Id = 1,
                FirstName = "Sullivan",
                LastName = "Lomas",
                Email = "user@website.com",
                Phone = "(702) 7768761",
                State = "NV"
            };
            return guest;
        }
    }
}
