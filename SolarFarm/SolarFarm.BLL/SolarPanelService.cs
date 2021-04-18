using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarFarm.Core;
using SolarFarm.DAL;

namespace SolarFarm.BLL
{
    public class SolarPanelService
	{
		SolarPanelRepository _repo;

		public SolarPanelService() 
		{
			_repo = new SolarPanelRepository("production.csv");
		}

		public Result<SolarPanel> Create(SolarPanel panel) 
		{

			//TODO Check for Duplicates
			Result<SolarPanel> result = new();
			result.Data = _repo.Create(panel);
			result.Success = true;
			result.Message = $"{panel} added.";
			return result;
		}

		public Result<List<SolarPanel>> ReadAll() 
		{
			Result<List<SolarPanel>> result = new ();
			result.Data = _repo.ReadAll();
			result.Success = true;
			result.Message = "Successfully Read From Repo.";
			return result;
		}

		public Result<List<SolarPanel>> ReadBySection(string section) 
		{
			//TODO Fail on empty section
			Result<List<SolarPanel>> result = new();
			result.Data = _repo.ReadBySection(section);
			result.Success = true;
			result.Message = "Successfully Read From Repo.";
			return result;
		}

		public Result<SolarPanel> ReadByPosition(string section, int row, int column) 
		{
			Result<SolarPanel> result = new();
			result.Data = _repo.ReadByPosition(section, row, column);
			result.Success = true;
			result.Message = "Successfully Read From Repo.";
			return result;
		}

		public Result<SolarPanel> Update(SolarPanel oldPanel, SolarPanel newPanel) 
		{
			Result<SolarPanel> result = new();
			result.Data = _repo.Update(oldPanel.Section, oldPanel.Row, oldPanel.Column, newPanel);
			result.Success = true;
			result.Message = $"{newPanel} updated.";
			return result;
		}

		public Result<SolarPanel> UpdateByPosition(string section, int row, int column, SolarPanel newPanel) 
		{
			Result<SolarPanel> result = new();
			result.Data = _repo.Update(section, row, column, newPanel);
			result.Success = true;
			result.Message = $"{newPanel} updated.";
			return result;
		}
	}
}
