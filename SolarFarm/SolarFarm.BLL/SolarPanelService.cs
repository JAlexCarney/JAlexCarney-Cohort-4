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
		private readonly SolarPanelRepository _repo;

		public SolarPanelService(string fileName) 
		{
			_repo = new SolarPanelRepository(fileName);
		}

		public Result<SolarPanel> Create(SolarPanel panel) 
		{
			Result<SolarPanel> result = new();
			
			if (_repo.ReadByPosition(panel.Section, panel.Row, panel.Column) != null) 
			{
				result.Data = null;
				result.Success = false;
				result.Message = $"{panel} already exists.";
				return result;
			}
			result.Data = _repo.Create(panel);
			result.Success = true;
			result.Message = $"{panel} added.";
			return result;
		}

		public Result<List<SolarPanel>> ReadAll() 
		{
			Result<List<SolarPanel>> result = new ();
			result.Data = _repo.ReadAll();
			if (result.Data.Count == 0)
			{
				result.Success = false;
				result.Message = $"No panels found.";
				return result;
			}
			result.Success = true;
			result.Message = "Successfully Read From Repo.";
			return result;
		}

		public Result<List<SolarPanel>> ReadBySection(string section) 
		{
			//TODO Fail on empty section
			Result<List<SolarPanel>> result = new();
			result.Data = _repo.ReadBySection(section);
			if (result.Data.Count == 0)
			{
				result.Success = false;
				result.Message = $"No panels found in {section}.";
				return result;
			}
			result.Success = true;
			result.Message = "Successfully Read From Repo.";
			return result;
		}

		public Result<SolarPanel> ReadByPosition(string section, int row, int column) 
		{
			Result<SolarPanel> result = new();
			result.Data = _repo.ReadByPosition(section, row, column);
			if (result.Data == null) 
			{
				result.Success = false;
				result.Message = $"Panel {section}-{row}-{column} not found.";
				return result;
			}
			result.Success = true;
			result.Message = "Successfully Read From Repo.";
			return result;
		}

		public Result<SolarPanel> Update(SolarPanel oldPanel, SolarPanel newPanel) 
		{
			return UpdateByPosition(oldPanel.Section, oldPanel.Row, oldPanel.Column, newPanel);
		}

		public Result<SolarPanel> UpdateByPosition(string section, int row, int column, SolarPanel newPanel) 
		{
			Result<SolarPanel> result = new();
			if (_repo.ReadByPosition(section, row, column) == null)
			{
				result.Success = false;
				result.Message = $"Panel {section}-{row}-{column} not found.";
				return result;
			}
			SolarPanel testPanel = _repo.ReadByPosition(newPanel.Section, newPanel.Row, newPanel.Column);
			if (testPanel != null)
			{
                if (testPanel.Section != section || testPanel.Row != row || testPanel.Column != column) 
				{
					result.Data = null;
					result.Success = false;
					result.Message = $"{newPanel} already exists.";
					return result;
				}
			}
			result.Data = _repo.Update(section, row, column, newPanel);
			result.Success = true;
			result.Message = $"{newPanel} updated.";
			return result;
		}

		public Result<SolarPanel> DeleteByPosition(string section, int row, int column) 
		{
			Result<SolarPanel> result = new();
			if (_repo.ReadByPosition(section, row, column) == null)
			{
				result.Data = null;
				result.Success = false;
				result.Message = $"There is no panel {section}-{row}-{column}.";
				return result;
			}
			result.Data = _repo.DeleteByPosition(section, row, column);
			result.Success = true;
			//TODO: Fail on non existent panel
			result.Message = $"{result.Data} removed.";
			return result;
		}
	}
}
