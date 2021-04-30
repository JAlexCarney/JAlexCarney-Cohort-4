using NUnit.Framework;
using DontWreckMyHouse.BLL.Test.TestDoubles;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Loggers;

namespace DontWreckMyHouse.BLL.Test
{
    class HostServiceTest
    {
        private HostService service;

        [SetUp]
        public void SetUp() 
        {
            service = new HostService(new HostRepositoryDouble(), new NullLogger());
        }

        [Test]
        public void ShouldReadAll()
        {
            // Arrange
            Host expected = HostRepositoryDouble.HOST;

            // Act
            var actual = service.ReadAll();

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(0, actual.Messages.Count);
            Assert.AreEqual(expected, actual.Data[0]);
        }

        [Test]
        public void ShouldReadHostByEmail()
        {
            // Arrange
            Host expected = HostRepositoryDouble.HOST;

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
            Assert.AreEqual("Failed to find host with that email.", actual.Messages[0]);
            Assert.IsNull(actual.Data);
        }
    }
}
