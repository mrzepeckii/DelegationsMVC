using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DelegationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _empService;

        public EmployeeController(IEmployeeService empService)
        {
            _empService = empService;
        }

        [HttpGet]
        public ActionResult<ListEmployeeForListVm> Get()
        {
            var result = _empService.GetAllEmployeeForList();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeDetailVm> Get(int id)
        {
            var result = _empService.GetEmployeeDetails(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
