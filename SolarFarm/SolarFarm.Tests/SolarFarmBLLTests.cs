using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarFarm.Core;
using SolarFarm.BLL;
using System.IO;

namespace SolarFarm.Tests
{
    [TestFixture]
    class SolarFarmBLLTests
    {
        [Test]
        public void ShouldErrorWhenReadingFromEmptyRepo() 
        {
            //Arrange

            //Act
            var service = new SolarPanelService("TestFileReading.csv");
            var resultOne = service.ReadAll();
            var resultTwo = service.ReadBySection("Ranch");
            var resultThree = service.ReadByPosition("Ranch", 1, 1);

            //Assert
            Assert.IsFalse(resultOne.Success);
            Assert.IsFalse(resultTwo.Success);
            Assert.IsFalse(resultThree.Success);

            //Clean Up
            File.Delete("TestFileReading.csv");
        }

        [Test]
        public void ShouldCreateEntryAndReadItBack() 
        {
            //Arrange
            var panel = new SolarPanel
            {
                Section = "West Field",
                Row = 20,
                Column = 30,
                Material = SolarPanelMaterial.MonoSi,
                YearInstalled = new DateTime(2020, 1, 1),
                IsTracking = false
            };

            //Act
            var service = new SolarPanelService("TestFileWriting.csv");
            var resultOne = service.Create(panel);
            var resultTwo = service.ReadAll();
            var resultThree = service.ReadBySection("West Field");
            var resultFour = service.ReadByPosition("West Field", 20, 30);

            //Assert
            Assert.That(resultOne.Success);
            Assert.AreEqual(resultOne.Data, panel);
            Assert.That(resultTwo.Success);
            Assert.AreEqual(resultTwo.Data.Count, 1);
            Assert.That(resultThree.Success);
            Assert.AreEqual(resultThree.Data.Count, 1);
            Assert.That(resultFour.Success);
            Assert.AreEqual(resultFour.Data, panel);

            //Clean Up
            File.Delete("TestFileWriting.csv");
        }

        [Test]
        public void ShouldDeleteEntryButFailToIfItDoesNotExist() 
        {
            //Arrange
            var panel = new SolarPanel
            {
                Section = "West Field",
                Row = 20,
                Column = 30,
                Material = SolarPanelMaterial.MonoSi,
                YearInstalled = new DateTime(2020, 1, 1),
                IsTracking = false
            };

            //Act
            var service = new SolarPanelService("TestFileDeleting.csv");
            service.Create(panel);
            var resultOne = service.DeleteByPosition("Ranch", 2, 2);
            var resultTwo = service.DeleteByPosition(panel.Section, panel.Row, panel.Column);
            var resultThree = service.DeleteByPosition(panel.Section, panel.Row, panel.Column);
            var resultFour = service.ReadAll();

            //Assert
            Assert.IsFalse(resultOne.Success);
            Assert.IsTrue(resultTwo.Success);
            Assert.IsFalse(resultThree.Success);
            Assert.AreEqual(resultFour.Data.Count, 0);

            //Clean Up
            File.Delete("TestFileDeleting.csv");
        }

        [Test]
        public void ShouldUpdateEntryButFailIfItDoesNotExist() 
        {
            //Arrange
            var panelOne = new SolarPanel
            {
                Section = "West Field",
                Row = 20,
                Column = 30,
                Material = SolarPanelMaterial.MonoSi,
                YearInstalled = new DateTime(2020, 1, 1),
                IsTracking = false
            };

            var panelTwo = new SolarPanel
            {
                Section = "West Field",
                Row = 25,
                Column = 30,
                Material = SolarPanelMaterial.MonoSi,
                YearInstalled = new DateTime(2020, 1, 1),
                IsTracking = false
            };

            var newPanel = new SolarPanel
            {
                Section = "WestField",
                Row = 60,
                Column = 55,
                Material = SolarPanelMaterial.AmoSi,
                YearInstalled = new DateTime(2018, 1, 1),
                IsTracking = false
            };

            //Act
            var service = new SolarPanelService("TestFileUpdating.csv");
            var resultOne = service.Update(panelOne, newPanel);
            service.Create(panelOne);
            service.Create(panelTwo);
            var resultTwo = service.Update(panelOne, newPanel);
            var resultThree = service.Update(newPanel, panelTwo);
            var resultFour = service.ReadAll();

            //Assert
            Assert.IsFalse(resultOne.Success);
            Assert.IsTrue(resultTwo.Success);
            Assert.IsFalse(resultThree.Success);
            Assert.AreEqual(2, resultFour.Data.Count);

            //Clean Up
            File.Delete("TestFileUpdating.csv");
        }
    }
}
