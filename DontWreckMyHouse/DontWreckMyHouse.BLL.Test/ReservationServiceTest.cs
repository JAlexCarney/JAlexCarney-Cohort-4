using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.BLL.Test.TestDoubles;
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
                new ReservationRepositoryDouble()
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
            Host emptyHost = new Host();
            emptyHost.Id = "test";

            // Act
            var actual = service.ReadByHost(emptyHost);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Failed to find any reservations for that host.", actual.Messages[0]);
            Assert.IsNull(actual.Data);
        }
    }
}
