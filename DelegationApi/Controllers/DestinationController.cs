using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DestinationVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DelegationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationService _destService;
        public DestinationController(IDestinationService destService)
        {
            _destService = destService; 
        }

        [HttpGet("Clients")]
        public ActionResult<ListDestinationForListVm> GetClients()
        {
            var result = _destService.GetAllClients();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("CurrentProjects")]
        public ActionResult<ListProjectForList> GetCurrenctProjects()
        {
            var result = _destService.GetCurrentProjects();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("Countries")]
        public ActionResult<List<CountryVm>> GetCountries()
        {
            var result = _destService.GetProjectsCountries().ToList();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("ClosedProjects")]
        public ActionResult<ListProjectForList> GetClosedProjects()
        {
            var result = _destService.GetClosedProjects();
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost("New")]
        public IActionResult AddDestination([FromBody]NewDestinationVm destVm)
        {
            if (!ModelState.IsValid || destVm.Id != 0)
            {
                return Conflict(ModelState);
            }
            _destService.AddDestination(destVm);
            return Ok();
        }

        [HttpPut("Edit/{id}")]
        public IActionResult EditDestination([FromBody]NewDestinationVm destVm)
        {
            var isExist = _destService.CheckIfDestinationExist(destVm.Id);
            if (!ModelState.IsValid || !isExist)
            {
                return Conflict(ModelState);
            }
            _destService.UpdateDesination(destVm);
            return Ok();
        }

        [HttpPost("Project/New")]
        public IActionResult AddProject([FromBody]NewProjectVm projVm)
        {
            if (!ModelState.IsValid || projVm.Id != 0)
            {
                return Conflict(ModelState);
            }
            _destService.AddProject(projVm);
            return Ok();
        }

        [HttpPut("Project/Edit/{id}")]
        public IActionResult EditProject([FromBody]NewProjectVm projVm)
        {
            var isExist = _destService.CheckIfProjectExist(projVm.Id);
            if (!ModelState.IsValid || !isExist)
            {
                return Conflict(ModelState);
            }
            _destService.UpdateProject(projVm);
            return Ok();
        }

        [HttpPost("Project/Close/{id}")]
        public IActionResult CloseProject(int id)
        {
            _destService.CloseProject(id);
            return Ok();
        }
    }
}
