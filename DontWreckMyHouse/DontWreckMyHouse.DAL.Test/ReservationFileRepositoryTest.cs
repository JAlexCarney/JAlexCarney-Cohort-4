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
    class ReservationFileRepositoryTest
    {
        private IReservationRepository repo;
        
        [SetUp]
        public void SetUp() 
        {
            repo = new ReservationFileRepository("TestReservations");
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
            Host emptyHost = new Host();
            emptyHost.Id = "Test";

            // Act
            var actual = repo.ReadByHost(emptyHost);

            // Assert
            Assert.IsNull(actual);
        }

        private Reservation MakeReservation() 
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

        private Host MakeHost()
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
