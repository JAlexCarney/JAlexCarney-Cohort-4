using System;
using NUnit.Framework;
using DontWreckMyHouse.BLL.Test.TestDoubles;
using DontWreckMyHouse.Core.Loggers;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL.Test
{
    class ReservationServiceTest
    {

        private ReservationService service;

        [SetUp]
        public void SetUp() 
        {
            service = new ReservationService
            (
                new HostRepositoryDouble(),
                new GuestRepositoryDouble(),
                new ReservationRepositoryDouble(),
                new NullLogger()
            );
        }

        [Test]
        public void ShouldReadByHost() 
        {
            // Arrange
            Reservation expected = ReservationRepositoryDouble.RESERVATION;

            // Act
            var actual = service.ReadByHost(HostRepositoryDouble.HOST);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(0, actual.Messages.Count);
            Assert.IsTrue(actual.Data.Contains(expected));
        }

        [Test]
        public void ShouldFailToReadByHostWithNoData() 
        {
            // Arrange
            var emptyHost = new Host
            {
                Id = "test"
            };

            // Act
            var actual = service.ReadByHost(emptyHost);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Failed to find any reservations for that host.", actual.Messages[0]);
            Assert.IsNull(actual.Data);
        }

        [Test]
        // Before existing Reservation
        [TestCase("2020-02-12", "2020-02-20")]
        // After existing Reservation
        [TestCase("2023-02-05", "2023-02-12")]
        public void ShouldCreateValidReservation(string startDate, string endDate) 
        {
            // Arrange
            var expected = new Reservation()
            {
                Id = 2,
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate),
                GuestId = 1,
                Total = 1022.50M
            };

            // Act
            var actual = service.Create(HostRepositoryDouble.HOST, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected, actual.Data);
        }

        [Test]
        public void ShouldReadAfterCreate() 
        {
            // Arrange
            var expected = new Reservation()
            {
                Id = 2,
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2001, 1, 1),
                GuestId = 1,
                Total = 1022.50M
            };

            // Act
            service.Create(HostRepositoryDouble.HOST, expected);
            var actual = service.ReadByHost(HostRepositoryDouble.HOST);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(2, actual.Data.Count);
            Assert.AreEqual(expected, actual.Data[1]);
        }

        [Test]
        public void ShouldFailToCreateNullReservation() 
        {
            // Act
            var actual = service.Create(HostRepositoryDouble.HOST, null);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Must provide a reservation", actual.Messages[0]);
        }

        [Test]
        public void ShouldFailToCreateReservationWithoutHost()
        {
            // Arrange
            var expected = new Reservation()
            {
                Id = 2,
                StartDate = new DateTime(2022, 2, 13),
                EndDate = new DateTime(2022, 2, 15),
                GuestId = 1,
                Total = 1022.50M
            };

            // Act
            var actual = service.Create(null, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Must provide a host.", actual.Messages[0]);
        }

        [Test]
        public void ShouldFailToCreateReservationWithUnknownHost()
        {
            // Arrange
            var expected = new Reservation()
            {
                Id = 2,
                StartDate = new DateTime(2022, 2, 13),
                EndDate = new DateTime(2022, 2, 15),
                GuestId = 1,
                Total = 1022.50M
            };

            // Act
            var actual = service.Create(new Host() { Id="test"}, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Host is not in database.", actual.Messages[0]);
        }

        [Test]
        public void ShouldFailToCreateReservationWithMissingData()
        {
            // Arrange
            var expected = new Reservation();

            // Act
            var actual = service.Create(HostRepositoryDouble.HOST, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Reservation is missing required fields.", actual.Messages[0]);
        }

        [Test]
        public void ShouldFailToCreateReservationWithInvalidDates()
        {
            // Arrange
            var expected = new Reservation()
            {
                Id = 2,
                StartDate = new DateTime(2025, 2, 13),
                EndDate = new DateTime(2022, 2, 15),
                GuestId = 1,
                Total = 1022.50M
            };

            // Act
            var actual = service.Create(HostRepositoryDouble.HOST, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("End date must be after start date.", actual.Messages[0]);
        }

        [Test]
        // existing date range is "2022-02-11" => "2022-02-13"
        // overlap exists if start date is within existing reservation
        [TestCase("2022-02-12", "2022-02-20")] 
        // overlap exists if end date is within existing reservation
        [TestCase("2022-02-05", "2022-02-12")] 
        // overlap exists if new reservation date range constains old reservation date range
        [TestCase("2022-01-01", "2022-12-01")]
        public void ShouldFailToCreateReservationWithOverlapingDates(string startDate, string endDate)
        {
            // Arrange
            var expected = new Reservation()
            {
                Id = 2,
                StartDate = DateTime.Parse(startDate),
                EndDate = DateTime.Parse(endDate),
                GuestId = 1,
                Total = 1022.50M
            };

            // Act
            var actual = service.Create(HostRepositoryDouble.HOST, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Date ranges can not overlap.", actual.Messages[0]);
        }

        [Test]
        public void ShouldFailToCreateReservationWithUnknownGuest() 
        {
            // Arrange
            var expected = new Reservation()
            {
                Id = 5,
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2001, 1, 1),
                GuestId = 2,
                Total = 1022.50M
            };

            // Act
            var actual = service.Create(HostRepositoryDouble.HOST, expected);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Guest data could not be found.", actual.Messages[0]);
        }

        [Test]
        public void ShouldDeleteReservation() 
        {
            // Act
            var actual = service.Delete(HostRepositoryDouble.HOST, ReservationRepositoryDouble.RESERVATION);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(ReservationRepositoryDouble.RESERVATION, actual.Data);
        }

        [Test]
        public void ShouldFailToDeleteFromUnknownHost() 
        {
            // Arrange
            var emptyHost = new Host
            {
                Id = "test"
            };

            // Act
            var actual = service.Delete(emptyHost, ReservationRepositoryDouble.RESERVATION);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("That host has no reservations.", actual.Messages[0]);
        }

        [Test]
        public void ShouldFailToDeleteUnknownReservation()
        {
            // Arrange
            var unkownReservation = new Reservation
            {
                Id = 3,
                StartDate = new DateTime(2131, 1, 1),
                EndDate = new DateTime(2132,1,1),
                GuestId = 2,
                Total = 5.0M
            };

            // Act
            var actual = service.Delete(HostRepositoryDouble.HOST, unkownReservation);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("That reservation does not exist with that host.", actual.Messages[0]);
        }

        [Test]
        public void ShouldFailToDeleteReservationInThePast()
        {
            // Arrange
            var reservation = new Reservation
            {
                Id = 3,
                StartDate = new DateTime(2, 1, 1),
                EndDate = new DateTime(3, 1, 1),
                GuestId = 1,
                Total = 5.0M
            };
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

            // Act
            service.Create(host, reservation);
            var actual = service.Delete(host, reservation);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Can not delete a past reservation.", actual.Messages[0]);
        }

        [Test]
        public void ShouldUpdateReservation() 
        {
            var expected = new Reservation()
            {
                Id = 1,
                StartDate = new DateTime(2030, 1, 1),
                EndDate = new DateTime(2031, 1, 1),
                GuestId = 1,
                Total = 1022.50M
            };

            var actual = service.Update(HostRepositoryDouble.HOST, ReservationRepositoryDouble.RESERVATION, expected);
            var actualStored = service.ReadByHost(HostRepositoryDouble.HOST);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(1, actualStored.Data.Count);
            Assert.AreEqual(expected, actual.Data);
            Assert.AreEqual(expected, actualStored.Data[0]);
        }

        [Test]
        public void ShouldFailToUpdateIntoInvalidEntry() 
        {
            var expected = new Reservation()
            {
                Id = 1,
                StartDate = new DateTime(2032, 1, 1),
                EndDate = new DateTime(2031, 1, 1),
                GuestId = 1,
                Total = 1022.50M
            };

            var actual = service.Update(HostRepositoryDouble.HOST, ReservationRepositoryDouble.RESERVATION, expected);
            var actualStored = service.ReadByHost(HostRepositoryDouble.HOST);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actualStored.Data.Count);
            Assert.IsNull(actual.Data);
            Assert.AreEqual(ReservationRepositoryDouble.RESERVATION, actualStored.Data[0]);
        }

        [Test]
        public void ShouldFailToUpdateIntoOverlappingDates()
        {
            var valid = new Reservation()
            {
                Id = 1,
                StartDate = new DateTime(2032, 1, 1),
                EndDate = new DateTime(2033, 1, 1),
                GuestId = 1,
                Total = 1022.50M
            };
            var expected = new Reservation()
            {
                Id = 1,
                StartDate = new DateTime(2032, 6, 6),
                EndDate = new DateTime(2034, 1, 1),
                GuestId = 1,
                Total = 1022.50M
            };

            service.Create(HostRepositoryDouble.HOST, valid);
            var actual = service.Update(HostRepositoryDouble.HOST, ReservationRepositoryDouble.RESERVATION, expected);
            var actualStored = service.ReadByHost(HostRepositoryDouble.HOST);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.IsNull(actual.Data);
            Assert.AreEqual("Date ranges can not overlap.", actual.Messages[0]);
            Assert.AreEqual(2, actualStored.Data.Count);
            Assert.IsTrue(actualStored.Data.Contains(valid));
        }

    }
}
