using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SolarFarm.DAL;
using SolarFarm.Core;
using System.IO;

namespace SolarFarm.Tests
{
    class SolarFarmDALTests
    {
        // Test file reading
        // Test file writing
        // Test returns
        // Test Read by Section
        // Test Read by ID

        SolarPanelRepository _repo;
        private const string SEED_FILE = "TestFileThree.csv";
        private const string FLUID_TEST_FILE = "FluidTestFile.csv";

        [OneTimeSetUp]
        public void Setup()
        {
            File.Copy(SEED_FILE, FLUID_TEST_FILE, true);
        }

        [Test]
        public void ShouldCreateFileIfNoneExists()
        {
            // Arrange
            string filename = "testFileOne.csv";

            // Act
            _repo = new SolarPanelRepository(filename);

            // Assert
            Assert.That(File.Exists(filename));

            // Clean Up
            File.Delete(filename);
        }

        [Test]
        public void ShouldCreateEntryAndGetEntryWhenGivenSolarPanel() 
        {
            // Arrange
            string filename = "testFileTwo.csv";
            SolarPanel panel = new SolarPanel();
            panel.Section = "West Field";
            panel.Row = 20;
            panel.Column = 30;
            panel.Material = SolarPanelMaterial.MonoSi;
            panel.YearInstalled = new DateTime(2020, 1, 1);
            panel.IsTracking = false;

            // Act
            _repo = new SolarPanelRepository(filename);
            _repo.Create(panel);
            List<SolarPanel> panels = _repo.ReadAll();

            // Assert
            Assert.AreEqual(panels.Count, 1);
            Assert.AreEqual(panel, panels[0]);

            // Clean Up
            File.Delete(filename);
        }

        [Test]
        public void ShouldReadDataAlreadyInFileAndReadByPositionAndSection() 
        {
            // Arrange
            SolarPanel expectedPanel = new SolarPanel();
            expectedPanel.Section = "WestField";
            expectedPanel.Row = 60;
            expectedPanel.Column = 55;
            expectedPanel.Material = SolarPanelMaterial.AmoSi;
            expectedPanel.YearInstalled = new DateTime(2018, 1, 1);
            expectedPanel.IsTracking = false;

            // Act
            SolarPanelRepository repo = new SolarPanelRepository(FLUID_TEST_FILE);
            SolarPanel testPanel = repo.ReadByPosition("WestField", 60, 55);

            // Assert
            Assert.IsNull(repo.ReadByPosition("WestField", 61, 55));
            Assert.AreEqual(testPanel.Section, expectedPanel.Section);
            Assert.AreEqual(testPanel.Row, expectedPanel.Row);
            Assert.AreEqual(testPanel.Column, expectedPanel.Column);
            Assert.AreEqual(testPanel.Material, expectedPanel.Material);
            Assert.AreEqual(testPanel.YearInstalled, expectedPanel.YearInstalled);
            Assert.AreEqual(testPanel.IsTracking, expectedPanel.IsTracking);
            Assert.AreNotEqual(repo.ReadByPosition("WestField", 10, 14), expectedPanel);
            Assert.AreEqual(repo.ReadAll().Count, 5);
            Assert.AreEqual(repo.ReadBySection("Ranch").Count, 2);
        }
    }
}
