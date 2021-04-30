using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DontWreckMyHouse.Core.Repositories;
using DontWreckMyHouse.Core.Loggers;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Exceptions;
using System.IO;

namespace DontWreckMyHouse.DAL.Test
{
    class ReservationFileRepositoryTest
    {
        private IReservationRepository repo;
        private const string DIRECTORY_NAME = "TestReservations";
        private const string TEST_FILE_PATH = @"TestReservations\8597c189-2352-49a2-ba9f-eb400d8dadbf.csv";
        private const string SEED_FILE_PATH = @"TestReservations\ReservationData.csv";

        [SetUp]
        public void SetUp() 
        {
            File.Copy(SEED_FILE_PATH, TEST_FILE_PATH, true);
            repo = new ReservationFileRepository(DIRECTORY_NAME, new NullLogger());
        }

        [Test]
        public void ShouldReadByHost() 
        {
            // Arrange
            Reservation expected = MakeReservation();
            Host host = MakeHost();

            // Act
            var actual = repo.ReadByHost(host);

            // Assert
            Assert.AreEqual(2, actual.Count);
            Assert.IsTrue(actual.Contains(expected));
        }

        [Test]
        public void ShouldFailToReadByInvalidHost()
        {
            // Arrange
            var emptyHost = new Host()
            {
                Id = "Test"
            };

            // Act
            var actual = repo.ReadByHost(emptyHost);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public void ShouldCreateReservationUnderHostWithUniqueId() 
        {
            // Arrange
            Reservation expected = MakeNewReservation();
            Host key = MakeHost();

            // Act
            Reservation actual = repo.Create(key, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Id);
        }

        [Test]
        public void ShouldReadReservationAfterItIsCreated() 
        {
            // Arrange
            Reservation expected = MakeNewReservation();
            Host key = MakeHost();

            // Act
            repo.Create(key, expected);
            List<Reservation> actual = repo.ReadByHost(key);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);
            Assert.IsTrue(actual.Contains(expected));
        }

        [Test]
        public void ShouldSaveToFileAfterCreate() 
        {
            // Arrange
            Reservation expected = MakeNewReservation();
            Host key = MakeHost();

            // Act
            repo.Create(key, expected);
            repo = new ReservationFileRepository(DIRECTORY_NAME, new NullLogger());
            List<Reservation> actual = repo.ReadByHost(key);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);
            Assert.IsTrue(actual.Contains(expected));
        }

        [Test]
        public void ShouldDeleteReservation() 
        {
            // Arrange
            Reservation expected = MakeReservation();
            Host key = MakeHost();

            // Act
            Reservation actual = repo.Delete(key, expected);
            var result = repo.ReadByHost(key);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(result.Contains(expected));
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ShouldDeleteFileWithNoReservations() 
        {
            // Arrange
            var reservatoinOne = MakeReservation();
            var reservationTwo = new Reservation
            {
                Id = 2,
                StartDate = new DateTime(2021, 11, 18),
                EndDate = new DateTime(2021, 11, 22),
                GuestId = 2,
                Total = 1840.50M
            };
            Host key = MakeHost();
            string filePath = Path.Combine(DIRECTORY_NAME, $"{key.Id}.csv");

            // Act
            repo.Delete(key, reservatoinOne);
            repo.Delete(key, reservationTwo);
            var result = repo.ReadByHost(key);

            // Assert
            Assert.IsNull(result);
            Assert.IsFalse(File.Exists(filePath));
        }

        [Test]
        public void ShouldThrowExceptionWhenReadingInvalidData() 
        {
            // Arrange
            var badRepo = new ReservationFileRepository("InvalidReservations", new NullLogger());

            // Assert
            Assert.Throws<RepositoryException>(() => { badRepo.ReadByHost(MakeHost()); });
        }

        [Test]
        public void ShouldThrowExeptionWhenTryingToReadFileWithoutPermission()
        {
            // Arrange
            var file = File.OpenWrite(@"TestReservations\8597c189-2352-49a2-ba9f-eb400d8dadbf.csv");

            // Assert
            Assert.Throws<RepositoryException>(() => { repo.ReadByHost(MakeHost()); });

            // CleanUp
            file.Close();
        }

        private static Reservation MakeReservation() 
        {
            var reservation = new Reservation
            {
                Id = 1,
                StartDate = new DateTime(2022, 2, 11),
                EndDate = new DateTime(2022, 2, 13),
                GuestId = 1,
                Total = 1022.50M
            };
            return reservation;
        }

        private static Reservation MakeNewReservation()
        {
            var reservation = new Reservation
            {
                Id = -1,
                StartDate = new DateTime(2025, 2, 11),
                EndDate = new DateTime(2025, 2, 13),
                GuestId = 1,
                Total = 1022.50M
            };
            return reservation;
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
