using FieldAgent.Core.Entities;
using FieldAgent.Core.DTOs;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldAgent.Web.Controllers.Model;

namespace FieldAgent.Web.Controllers
{
    public class ReportsController : Controller
    {
        private IReportsRepository _reportsRepository;

        public ReportsController(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        [Route("reports")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("reports/topAgents")]
        [HttpGet]
        public IActionResult TopAgentsReport()
        {
            var response = _reportsRepository.GetTopAgents();
            if (!response.Success) 
            {
                throw new Exception(response.Message);
            }
            return View(response.Data);
        }

        [Route("reports/pension")]
        [HttpGet]
        public IActionResult PensionReport()
        {
            return View();
        }

        [Route("reports/pensionList")]
        [HttpGet]
        public IActionResult PensionReportList(IdModel model)
        {
            if (model.Id == 0) 
            {
                ModelState.AddModelError("Id", "Agency Id is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _reportsRepository.GetPensionList(model.Id);
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }
            return View(response.Data);
        }

        [Route("reports/clearanceAudit")]
        [HttpGet]
        public IActionResult ClearanceAuditReport()
        {
            return View();
        }

        [Route("reports/clearanceAuditList")]
        [HttpGet]
        public IActionResult ClearanceAuditReportList(IdModel model)
        {
            if (model.Id == 0)
            {
                ModelState.AddModelError("Id", "Security Clearance Id is required");
            }
            
            if (!model.Id2.HasValue)
            {
                ModelState.AddModelError("Id", "Agency Id is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _reportsRepository.AuditClearance(model.Id, model.Id2.Value);
            if (!response.Success)
            {
                throw new Exception(response.Message);
            }
            return View(response.Data);
        }
    }
}
