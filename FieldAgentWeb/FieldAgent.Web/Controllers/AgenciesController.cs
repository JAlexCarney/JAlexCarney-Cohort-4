using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldAgent.Web.Controllers
{
    public class AgenciesController : Controller
    {
        private IAgencyRepository _agencyRepository;

        public AgenciesController(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }

        [Route("agencies")]
        [HttpGet]
        public IActionResult List()
        {
            var result = _agencyRepository.GetAll();

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        [Route("agencies/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _agencyRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        [Route("agencies/add")]
        [HttpGet]
        public IActionResult Add()
        {
            var model = new Agency();
            return View(model);
        }

        [Route("agencies/add")]
        [HttpPost]
        public IActionResult Add(Agency model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _agencyRepository.Insert(model);

            if (result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                // todo: add validation messages to form later
                throw new Exception(result.Message);
            }
        }

        [Route("agencies/edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _agencyRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        [Route("agencies/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(Agency model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _agencyRepository.Update(model);

            if (result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                // todo: add validation messages to form later
                throw new Exception(result.Message);
            }
        }

        [Route("agencies/remove/{id}")]
        [HttpGet]
        public IActionResult Remove(int id)
        {
            var result = _agencyRepository.Get(id);

            if (result.Success)
            {
                return View(result.Data);
            }
            else
            {
                throw new Exception(result.Message);
            }
        }

        [Route("agencies/remove/{id}")]
        [HttpPost]
        public IActionResult Remove(Agency model)
        {
            var result = _agencyRepository.Delete(model.AgencyId);

            if (result.Success)
            {
                return RedirectToAction("List");
            }
            else
            {
                throw new Exception(result.Message);
            }
        }
    }
}
