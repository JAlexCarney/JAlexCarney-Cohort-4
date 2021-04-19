using System;
using System.Collections.Generic;
using SolarFarm.Core;
using SolarFarm.BLL;

namespace SolarFarm.UI
{
    public class SolarFarmController
    {
        private SolarPanelService _service;
        public void Run() 
        {
            // Create Service Objcet
            _service = new SolarPanelService("production.csv");

            // Display Intro Header To User
            ConsoleIO.DisplayHeader("Welcome to Solar Farm");

            // Enter The Menu Loop
            MenuLoop();
        }

		public void MenuLoop() 
        {
            // Start MenuLoop
            while (true)
            {
                // Display Menu Options
                ConsoleIO.DisplayMenuOptions(
                    "Main Menu",
                    "Exit",
                    "Find Panels by Section",
                    "Add a Panel",
                    "Update a Panel",
                    "Remove a Panel");

                // Prompt User Choice
                int choice = ConsoleIO.GetIntInRange("Select [0-4]: ", 0, 4);
                
                // switch on choice
                switch (choice)
                {
                    case 0:
                        // Exit
                        return;

                    case 1:
                        // Find Panels by Section
                        ConsoleIO.DisplayHeader("Find Panels by Section");
                        // Read from the repo using the service object
                        Result<List<SolarPanel>> readResult = _service.ReadBySection(ConsoleIO.GetString("Section Name: "));
                        // Check Result
                        if (readResult.Success)
                        {
                            // Display the panels found
                            ConsoleIO.DisplaySolarPanelList(readResult.Data);
                        }
                        else 
                        {
                            ConsoleIO.DisplayFailure(readResult.Message);
                        }
                        break;

                    case 2:
                        // Add a Panel
                        ConsoleIO.DisplayHeader("Add a Panel");
                        // Create Panel in Database using Service
                        Result<SolarPanel> creationResult = _service.Create(ConsoleIO.GetSolarPanel());
                        if (creationResult.Success)
                        {
                            ConsoleIO.DisplaySuccess(creationResult.Message);
                        }
                        else
                        {
                            ConsoleIO.DisplayFailure(creationResult.Message);
                        }
                        break;

                    case 3:
                        // Update a Panel
                        ConsoleIO.DisplayHeader("Update a Panel");
                        // Get the panel from the repo that is going to be updated
                        Result<SolarPanel> panelToUpdateResult = _service.ReadByPosition(
                            ConsoleIO.GetString("Section: "),
                            ConsoleIO.GetIntInRange("Row: ", 1, 250),
                            ConsoleIO.GetIntInRange("Column: ", 1, 250));
                        Console.WriteLine();
                        if (panelToUpdateResult.Success)
                        {
                            SolarPanel newPanel = ConsoleIO.UpdatePanel(panelToUpdateResult.Data);

                            Result<SolarPanel> panelUpdatedResult = _service.Update(panelToUpdateResult.Data, newPanel);

                            if (panelUpdatedResult.Success)
                            {
                                ConsoleIO.DisplaySuccess(panelUpdatedResult.Message);
                            }
                            else 
                            {
                                ConsoleIO.DisplayFailure(panelUpdatedResult.Message);
                            }
                        }
                        else 
                        {
                            ConsoleIO.DisplayFailure(panelToUpdateResult.Message);
                        }
                        break;

                    case 4:
                        // Remove a Panel
                        ConsoleIO.DisplayHeader("Remove a Panel");
                        // Create Panel in Database using Service
                        Result<SolarPanel> removeResult = _service.DeleteByPosition(
                            ConsoleIO.GetString("Section: "),
                            ConsoleIO.GetIntInRange("Row: ", 1, 250),
                            ConsoleIO.GetIntInRange("Column: ", 1, 250));
                        Console.WriteLine();
                        if (removeResult.Success)
                        {
                            ConsoleIO.DisplaySuccess(removeResult.Message);
                        }
                        else
                        {
                            ConsoleIO.DisplayFailure(removeResult.Message);
                        }
                        break;
                }

                // Prompt Continue so user can see printed output before clear
                ConsoleIO.PromptContinueAndClear();
            }
        }
    }
}
