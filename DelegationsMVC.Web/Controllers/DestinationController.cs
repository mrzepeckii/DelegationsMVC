using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DelegationsMVC.Web.Controllers
{
    [Authorize( Roles = "Admin, Chief")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destService;
        private readonly ILogger<DestinationController> _logger;
        public DestinationController(IDestinationService destService, ILogger<DestinationController> logger)
        {
            _destService = destService;
            _logger = logger;
        }

        [Route("Destinations/All")]
        public IActionResult Index()
        {
            var model = _destService.GetAllClients();
            return View(model);
        }

        [HttpGet]
        [Route("Destinations/New")]
        public IActionResult AddDestination()
        {
            var model = new NewDestinationVm()
            {
                Countries = _destService.GetCountries().ToList()
            };
            return PartialView(model);
        }

        [HttpPost]
        [Route("Destinations/New")]
        public IActionResult AddDestination(NewDestinationVm destVm)
        {
            if (!ModelState.IsValid)
            {
                destVm.Countries = _destService.GetCountries().ToList();
                return PartialView(destVm);
            }
            var id = _destService.AddDestination(destVm);
            _logger.LogInformation("New destination has been added - " + id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Destinations/Edit/{id}")]
        public IActionResult EditDestination(int id)
        {
            var model = _destService.GetDestinationForEdit(id);
            if(model == null)
            {
                _logger.LogInformation("Can't edit destination - dosen't exist");
                return RedirectToAction("Index");
            }
            return PartialView(model);
        }

        [HttpPost]
        [Route("Destinations/Edit/{id}")]
        public IActionResult EditDestination(NewDestinationVm destVm)
        {
            if (!ModelState.IsValid)
            {
                destVm.Countries = _destService.GetCountries().ToList();
                return PartialView(destVm);
            }
            _destService.UpdateDesination(destVm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Destinations/Delete/{id}")]
        public IActionResult DeleteDestination(int id)
        {
            _destService.DeleteDestination(id);
            _logger.LogInformation("Destination " + id + " has been deleted");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Destinations/View/{id}")]
        public IActionResult ViewDestination(int id)
        {
            var model = _destService.GetDestinationDetail(id);
            if (model == null)
            {
                _logger.LogInformation("Can't find destination - " + id + " - dosen't exist");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Route("Destinations/Projects/All")]
        public IActionResult Projects()
        {
            var model = _destService.GetAllProjects();
            return View(model);
        }

        [HttpGet]
        [Route("Destinations/Projects/New")]
        public IActionResult AddProject()
        {
            var model = new NewProjectVm()
            {
                Destinations = _destService.GetDestinations().ToList()
            };
            return PartialView(model);
        }

        [HttpPost]
        [Route("Destinations/Projects/New")]
        public IActionResult AddProject(NewProjectVm projVm)
        {
            if (!ModelState.IsValid)
            {
                projVm.Destinations = _destService.GetDestinations().ToList();
                return PartialView(projVm);
            }
            _destService.AddProject(projVm);
            return RedirectToAction("Projects");
        }

        [HttpGet]
        [Route("Destinations/Projects/Edit/{id}")]
        public IActionResult EditProject(int id)
        {
            var model = _destService.GetProjectForEdit(id);
            if (model == null)
            {
                _logger.LogInformation("Can't edit project - dosen't exist");
                return RedirectToAction("Projects");
            }
            return PartialView(model);
        }

        [HttpPost]
        [Route("Destinations/Projects/Edit/{id}")]
        public IActionResult EditProject(NewProjectVm projVm)
        {
            if (!ModelState.IsValid)
            {
                projVm.Destinations = _destService.GetDestinations().ToList();
                return PartialView(projVm);
            }
            _destService.UpdateProject(projVm);
            return RedirectToAction("Projects");
        }

        [HttpGet]
        [Route("Destinations/Projects/Delete/{id}")]
        public IActionResult DeleteProject(int id)
        {
            _destService.DeleteProject(id);
            return RedirectToAction("Projects");
        }
    }
}
