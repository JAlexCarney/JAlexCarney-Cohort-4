using NUnit.Framework;
using DontWreckMyHouse.BLL.Test.TestDoubles;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Loggers;

namespace DontWreckMyHouse.BLL.Test
{
    class GuestServiceTest
    {
        private GuestService service;

        [SetUp]
        public void SetUp()
        {
            service = new GuestService(new GuestRepositoryDouble(), new NullLogger());
        }

        [Test]
        public void ShouldReadAll()
        {
            // Arrange
            Guest expected = GuestRepositoryDouble.GUEST;

            // Act
            var actual = service.ReadAll();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(0, actual.Messages.Count);
            Assert.AreEqual(expected, actual.Data[0]);
        }

        [Test]
        public void ShouldReadGuestByEmail()
        {
            // Arrange
            Guest expected = GuestRepositoryDouble.GUEST;

            // Act
            var actual = service.ReadByEmail(expected.Email);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(0, actual.Messages.Count);
            Assert.AreEqual(expected, actual.Data);
        }

        [Test]
        public void ShouldFailToReadIfEmailNotFound()
        {
            // Act
            var actual = service.ReadByEmail("test@test.com");

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Failed to find guest with that email.", actual.Messages[0]);
            Assert.IsNull(actual.Data);
        }

        [Test]
        public void ShouldReadGuestById()
        {
            // Arrange
            Guest expected = GuestRepositoryDouble.GUEST;

            // Act
            var actual = service.ReadById(expected.Id);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(0, actual.Messages.Count);
            Assert.AreEqual(expected, actual.Data);
        }

        [Test]
        public void ShouldFailToReadIfIdNotFound()
        {
            // Act
            var actual = service.ReadById(271);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Failed to find guest with that ID.", actual.Messages[0]);
            Assert.IsNull(actual.Data);
        }
    }
}