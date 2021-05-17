using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.DAL.Repos;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Tests
{
    class ADOReportsRepositoryTests
    {
        private ADOReportsRepository repo;

        [OneTimeSetUp]
        public void Setup()
        {
            repo = new ADOReportsRepository(@"Server=localhost;Database=FieldAgent;User Id=sa;Password=YOUR_strong_*pass4w0rd*");
        }

        [Test]
        public void ShouldReportTopAgents() 
        {
            // Act
            var response = repo.GetTopAgents();

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(3, response.Data.Count());
        }

        [Test]
        public void ShouldReportPension() 
        {
            // Act
            var response = repo.GetPensionList(5);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(2, response.Data.Count());
        }

        [Test]
        public void ShouldReportClearanceAudit() 
        {
            // Act
            var response = repo.AuditClearance(4, 1);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count());
        }
    }
}
