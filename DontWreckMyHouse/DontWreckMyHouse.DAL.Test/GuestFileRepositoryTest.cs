using NUnit.Framework;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Loggers;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Exceptions;
using System.IO;

namespace DontWreckMyHouse.DAL.Test
{
    class GuestFileRepositoryTest
    {
        private IGuestRepository repo;

        [SetUp]
        public void SetUp()
        {
            repo = new GuestFileRepository("TestGuests.csv", new NullLogger());
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

        [Test]
        public void ShouldReadByID()
        {
            // Arrange
            var expected = MakeGuest();

            // Act
            var actual = repo.ReadById(1);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldFailToReadIfIDNotFound()
        {
            // Act
            var actual = repo.ReadById(27);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public void ShouldThrowExeptionWhenReadingInvalidData() 
        {
            // Arrange
            var badRepo = new GuestFileRepository("InvalidGuests.csv", new NullLogger());
            
            // Assert
            Assert.Throws<RepositoryException>(() => { badRepo.ReadAll(); });
        }

        [Test]
        public void ShouldThrowExeptionWhenTryingToReadFileWithoutPermission() 
        {
            // Arrange
            var file = File.OpenWrite("TestGuests.csv");

            // Assert
            Assert.Throws<RepositoryException>(() => { repo.ReadAll(); });

            // CleanUp
            file.Close();
        }

        private static Guest MakeGuest()
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
