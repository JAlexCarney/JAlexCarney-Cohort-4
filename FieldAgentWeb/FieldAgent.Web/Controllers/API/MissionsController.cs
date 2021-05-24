using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldAgent.Web.Controllers
{
    [ApiController]
    public class MissionsController : ControllerBase
    {
        IMissionRepository _missionRepository;

        public MissionsController(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        [HttpGet("/api/missions/{id}", Name = "GetMission"), Authorize]
        public IActionResult GetMission(int id)
        {
            var result = _missionRepository.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("/api/missions/byagency/{id}"), Authorize]
        public IActionResult GetMissionsByAgnecy(int id)
        {
            var result = _missionRepository.GetByAgency(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("/api/missions/byagent/{id}"), Authorize]
        public IActionResult GetMissionsByAgent(int id)
        {
            var result = _missionRepository.GetByAgent(id);
            
            if (result.Success)
            {
                return Ok(result.Data
                    .Select(m => new 
                    {
                        m.MissionId,
                        m.AgencyId,
                        m.CodeName,
                        m.StartDate,
                        m.ProjectedEndDate,
                        m.ActualEndDate,
                        m.OperationalCost,
                        m.Notes
                    }));
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("/api/missions"), Authorize]
        public IActionResult AddMission(Mission mission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _missionRepository.Insert(mission);

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetMission), new { id = mission.MissionId }, mission);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("/api/missions"), Authorize]
        public IActionResult EditMission(Mission mission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_missionRepository.Get(mission.MissionId).Success)
            {
                return NotFound($"Mission {mission.MissionId} not found");
            }

            var result = _missionRepository.Update(mission);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("/api/missions/{id}"), Authorize]
        public IActionResult DeleteMission(int id)
        {
            if (!_missionRepository.Get(id).Success)
            {
                return NotFound($"Mission {id} not found");
            }

            var result = _missionRepository.Delete(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
