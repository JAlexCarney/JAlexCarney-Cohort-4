using NUnit.Framework;
using DontWreckMyHouse.BLL.Test.TestDoubles;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.BLL.Test
{
    class GuestServiceTest
    {
        private GuestService service;

        [SetUp]
        public void SetUp()
        {
            service = new GuestService(new GuestRepositoryDouble());
        }

        [Test]
        public void ShouldReadHostByEmail()
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
    }
}