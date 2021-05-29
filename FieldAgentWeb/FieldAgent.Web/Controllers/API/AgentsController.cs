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
    public class AgentsController : Controller
    {
        private IAgentRepository _agentRepository;

        public AgentsController(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        [HttpGet("/api/agents"), Authorize]
        public IActionResult GetAllAgents()
        {
            var result = _agentRepository.GetAll();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("/api/agents/{id}", Name = "GetAgent"), Authorize]
        public IActionResult GetAgent(int id)
        {
            var result = _agentRepository.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("/api/agents/{id}/missions"), Authorize]
        public IActionResult GetAgentMissions(int id)
        {
            var result = _agentRepository.GetMissions(id);

            if (result.Success)
            {
                return Ok(result.Data.Select(m => new
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

        [HttpPost("/api/agents"), Authorize]
        public IActionResult AddAgent([ModelBinder] Agent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _agentRepository.Insert(agent);

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetAgent), new { id = agent.AgentId }, agent);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("/api/agents"), Authorize]
        public IActionResult EditAgent([ModelBinder] Agent agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_agentRepository.Get(agent.AgentId).Success)
            {
                return NotFound($"Agent {agent.AgentId} not found");
            }

            var result = _agentRepository.Update(agent);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("/api/agents/{id}"), Authorize]
        public IActionResult DeleteAgent(int id)
        {
            if (!_agentRepository.Get(id).Success)
            {
                return NotFound($"Agent {id} not found");
            }

            var result = _agentRepository.Delete(id);

            if (result.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
