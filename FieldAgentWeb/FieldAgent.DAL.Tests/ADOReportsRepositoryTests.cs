using NUnit.Framework;
using System.Linq;
using FieldAgent.DAL.Repos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Reflection;

[assembly: UserSecretsId("29e65d1a-e03e-441b-a297-7a3afe173669")]
namespace FieldAgent.DAL.Tests
{
    class ADOReportsRepositoryTests
    {
        private ADOReportsRepository repo;

        [OneTimeSetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder();

            builder.AddUserSecrets(typeof(ADOReportsRepositoryTests).GetTypeInfo().Assembly);

            var config = builder.Build();

            var connectionString = config["ConnectionStrings:FieldAgent"];
            //string connectionString = @"Server=localhost;Database=FieldAgent;User Id=sa;Password=YOUR_strong_*pass4w0rd*";
            repo = new ADOReportsRepository(connectionString);
        }

        [Test]
        public void ShouldReportTopAgents() 
        {
            // Act
            var response = repo.GetTopAgents();

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(3, response.Data.Count);
        }

        [Test]
        public void ShouldReportPension() 
        {
            // Act
            var response = repo.GetPensionList(5);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(2, response.Data.Count);
        }

        [Test]
        public void ShouldReportClearanceAudit() 
        {
            // Act
            var response = repo.AuditClearance(4, 1);

            // Assert
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Message);
            Assert.AreEqual(1, response.Data.Count);
        }
    }
}
