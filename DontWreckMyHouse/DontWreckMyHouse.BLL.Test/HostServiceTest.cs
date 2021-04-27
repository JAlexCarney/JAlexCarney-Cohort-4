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
    class HostServiceTest
    {
        private HostService service;

        [SetUp]
        public void SetUp() 
        {
            service = new HostService(new HostRepositoryDouble());
        }

        [Test]
        public void ShouldReadHostByEmail()
        {
            // Arrange
            Host expected = HostRepositoryDouble.HOST;

            // Act
            var actual = service.ReadHostByEmail(expected.Email);

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
            var actual = service.ReadHostByEmail("test@test.com");

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Success);
            Assert.AreEqual(1, actual.Messages.Count);
            Assert.AreEqual("Failed to find host with that email.", actual.Messages[0]);
            Assert.IsNull(actual.Data);
        }
    }
}
