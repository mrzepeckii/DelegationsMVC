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

        [HttpGet]
        public ActionResult<ListDestinationForListVm> GetClients()
        {
            var result = _destService.GetAllClients();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<ListProjectForList> GetCurrenctProjects()
        {
            var result = _destService.GetCurrentProjects();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<CountryVm>> GetCountries()
        {
            var result = _destService.GetProjectsCountries().ToList();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<ListProjectForList> GetClosedProjects()
        {
            var result = _destService.GetClosedProjects();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}
