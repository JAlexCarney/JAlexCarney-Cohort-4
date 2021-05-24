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
    public class AliasesController : ControllerBase
    {
        IAliasRepository _aliasRepository;

        public AliasesController(IAliasRepository aliasRepository)
        {
            _aliasRepository = aliasRepository;
        }

        [HttpGet("/api/aliases/{id}", Name = "GetAlias"), Authorize]
        public IActionResult GetAlias(int id)
        {
            var result = _aliasRepository.Get(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("/api/aliases/byagent/{id}"), Authorize]
        public IActionResult GetAliasByAgent(int id)
        {
            var result = _aliasRepository.GetByAgent(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("/api/aliases"), Authorize]
        public IActionResult AddAlias(Alias alias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _aliasRepository.Insert(alias);

            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetAlias), new { id = alias.AliasId }, alias);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("/api/aliases"), Authorize]
        public IActionResult EditAlias(Alias alias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_aliasRepository.Get(alias.AliasId).Success)
            {
                return NotFound($"Alias {alias.AliasId} not found");
            }

            var result = _aliasRepository.Update(alias);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("/api/aliases/{id}"), Authorize]
        public IActionResult DeleteAlias(int id)
        {
            if (!_aliasRepository.Get(id).Success)
            {
                return NotFound($"Alias {id} not found");
            }

            var result = _aliasRepository.Delete(id);

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
