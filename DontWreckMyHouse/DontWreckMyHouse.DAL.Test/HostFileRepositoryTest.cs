using NUnit.Framework;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Loggers;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Exceptions;
using System.IO;

namespace DontWreckMyHouse.DAL.Test
{
    class HostFileRepositoryTest
    {
        private IHostRepository repo;

        [SetUp]
        public void SetUp() 
        {
            repo = new HostFileRepository("TestHosts.csv", new NullLogger());
        }

        [Test]
        public void ShouldReadAll() 
        {
            // Arrange
            var expected = MakeHost();

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
            var expected = MakeHost();

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
        public void ShouldThrowExeptionWhenReadingInvalidData()
        {
            var badRepo = new HostFileRepository("InvalidHosts.csv", new NullLogger());

            Assert.Throws<RepositoryException>(() => { badRepo.ReadAll(); });
        }

        [Test]
        public void ShouldThrowExeptionWhenTryingToReadFileWithoutPermission()
        {
            // Arrange
            var file = File.OpenWrite("TestHosts.csv");

            // Assert
            Assert.Throws<RepositoryException>(() => { repo.ReadAll(); });

            // CleanUp
            file.Close();
        }

        private static Host MakeHost() 
        {
            var host = new Host
            {
                Id = "8597c189-2352-49a2-ba9f-eb400d8dadbf",
                LastName = "Folkerd",
                Email = "user@website.com",
                Phone = "(281) 1808157",
                Address = "59778 Clove Road",
                City = "Houston",
                State = "TX",
                PostalCode = 77075,
                StandardRate = 285M,
                WeekendRate = 356.25M
            };
            return host;
        }
    }
}
