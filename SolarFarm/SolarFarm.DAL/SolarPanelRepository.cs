using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarFarm.Core;

namespace SolarFarm.DAL
{
    /// <summary>
    /// Stores and retrieves solar panel data from a file
    /// using the format
    ///     Section,Row,Column,Material,InstallationYear,Tracked
    /// </summary>
    public class SolarPanelRepository
    {
        private readonly string _fileName;
        private readonly Dictionary<SolarPanelIdentifier, SolarPanel> _panels;

        /// <summary>
        /// Creates a new SolarPanelRepository with a file for saving data persitantly
        /// </summary>
        /// <param name="filename">The name of the file to use for the repo, can already have data</param>
        public SolarPanelRepository(string filename) 
        {
            _fileName = filename;
            _panels = new Dictionary<SolarPanelIdentifier, SolarPanel>();
            LoadFromFile();
        }

        /// <summary>
        /// Creates a new data entry for a solar panel and stores it
        /// </summary>
        /// <param name="panel">The panel to be stored</param>
        /// <returns>The panel that was stored</returns>
        public SolarPanel Create(SolarPanel panel) 
        {
            // Add panel to data scructure
            _panels.Add(
                new SolarPanelIdentifier(panel.Section, panel.Row, panel.Column),
                panel);
            // Save updated data to file
            SaveToFile();
            // Success!
            return panel;
        }

        /// <summary>
        /// Gets all of the data from the database in a single list
        /// </summary>
        /// <returns>A list of all Solar Panels in the database</returns>
        public List<SolarPanel> ReadAll() 
        {
            return _panels.Values.ToList();
        }

        /// <summary>
        /// Gets all the solar panels in a given section
        /// </summary>
        /// <param name="section">The section to get panels from</param>
        /// <returns></returns>
        public List<SolarPanel> ReadBySection(string section) 
        {
            List<SolarPanel> FoundPanels = new List<SolarPanel>();
            foreach (SolarPanel panel in _panels.Values) 
            {
                if (panel.Section == section) 
                {
                    FoundPanels.Add(panel);
                }
            }
            return FoundPanels;
        }

        /// <summary>
        /// Get a specific Panel from the database
        /// </summary>
        /// <param name="section">section of panel to get</param>
        /// <param name="row">row of panel to get</param>
        /// <param name="column">column of panel to get</param>
        /// <returns></returns>
        public SolarPanel ReadByPosition(string section, int row, int column) 
        {
            SolarPanelIdentifier iD = new SolarPanelIdentifier(section, row, column);
            if (_panels.ContainsKey(iD)) 
            {
                return _panels[iD];
            }
            return null;
        }

        public SolarPanel Update(string section, int row, int column, SolarPanel panel) 
        {
            _panels[new SolarPanelIdentifier(section, row, column)] = panel;
            // Save updated data to file
            SaveToFile();
            return panel;
        }

        /// <summary>
        /// Deletes a Solar panel from the data scructure
        /// </summary>
        /// <param name="panel"></param>
        /// <returns>The deleted panel</returns>
        public SolarPanel Delete(SolarPanel panel) 
        {
            _panels.Remove(new SolarPanelIdentifier(panel.Section, panel.Row, panel.Column));
            // Save updated data to file
            SaveToFile();
            return panel;
        }

        /// <summary>
        /// Deletes a 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public SolarPanel DeleteByPosition(string section, int row, int column)
        {
            SolarPanelIdentifier iD = new SolarPanelIdentifier(section, row, column);
            SolarPanel panel = _panels[iD];
            _panels.Remove(iD);
            // Save updated data to file
            SaveToFile();
            return panel;
        }

        /// <summary>
        /// Loads data from the associated file
        /// </summary>
        private void LoadFromFile() 
        {
            if (File.Exists(_fileName))
            {
                using (StreamReader streamReader = new StreamReader(_fileName))
                {
                    for (string dataEntry = streamReader.ReadLine(); dataEntry != null; dataEntry = streamReader.ReadLine())
                    {
                        //Loaded as -> "Section,Row,Column,Material,InstallationYear,IsTracking"
                        //Input validation handeled in BLL and UI layers
                        string[] dataFields = dataEntry.Split(',');

                        // Create Solar Panel object
                        SolarPanel panel = new SolarPanel();
                        panel.Section = dataFields[0];
                        panel.Row = int.Parse(dataFields[1]);
                        panel.Column = int.Parse(dataFields[2]);
                        panel.Material = Enum.Parse<SolarPanelMaterial>(dataFields[3]);
                        panel.YearInstalled = new DateTime(int.Parse(dataFields[4]), 1, 1);
                        panel.IsTracking = bool.Parse(dataFields[5]);

                        // Add object to internal data structure
                        _panels.Add(
                            new SolarPanelIdentifier(panel.Section, panel.Row, panel.Column),
                            panel);
                    }
                }
            }
            else 
            {
                File.Create(_fileName).Close();
            }
        }

        /// <summary>
        /// Saves data currently in the Dictionary to a file
        /// </summary>
        private void SaveToFile() 
        {
            // Check if file is present
            if (File.Exists(_fileName))
            {
                // Write objects to file one per line
                using (StreamWriter streamWriter = new StreamWriter(_fileName))
                {
                    foreach (SolarPanel panel in _panels.Values)
                    {
                        //Saved as -> "Section,Row,Column,Material,InstallationYear,IsTracking"
                        streamWriter.WriteLine(
                            $"{panel.Section},{panel.Row},{panel.Column},{panel.Material},{panel.YearInstalled},{panel.IsTracking}");
                    }
                }
            }
        }
    }
}
