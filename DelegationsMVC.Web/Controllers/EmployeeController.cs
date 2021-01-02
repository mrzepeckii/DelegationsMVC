using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace DelegationsMVC.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService empService, ILogger<EmployeeController> logger)
        {
            _empService = empService;
            _logger = logger;
        }

        [Authorize( Roles = "Accountant, Chief, Admin")]
        [Route("Employee/All")]
        public IActionResult Index()
        {
            var employees = _empService.GetAllEmployeeForList();
            return View(employees);
        }

        /****Employe****/

        [Route("Employee/New")]
        public IActionResult AddEmployee()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (!_empService.CheckIfEmployeeExist(userId))
            {
                return RedirectToAction("ViewEmployee");
            }
            var model = new NewEmployeeVm()
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            _empService.SetParametersToVm(model);
            return View(model);
        }

        [HttpPost]
        [Route("Employee/New")]
        public IActionResult AddEmployee(NewEmployeeVm model)
        {
            if (!ModelState.IsValid)
            {
                _empService.SetParametersToVm(model);
                return View(model);
            }
            model.Vehicles = _empService.CheckVehiclesList(model.Vehicles);
            model.ContactDetails = _empService.CheckContactsList(model.ContactDetails);
            var id = _empService.AddEmployee(model);
            _logger.LogInformation("New employee has been added - " + id);
            return RedirectToAction("ViewEmployee");
        }

        [HttpGet]
        [Authorize(Roles = "Accountant, Chief, Admin")]
        [Route("Employee/Profile/{id}")]
        public IActionResult ViewEmployee(int id)
        {
            var empVm = _empService.GetEmployeeDetails(id);
            if (empVm == null)
            {
                _logger.LogInformation("Can't show employee details - employee dosen't exist");
                return RedirectToAction("Index");
            }
            return View(empVm);
        }

        [Route ("Employee/Profile")]
        public IActionResult ViewEmployee()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var empVm = _empService.GetEmployeeDetails(userId);
            if(empVm == null)
            {
                _logger.LogInformation("Can't show employee details - employee dosen't exist");
                return RedirectToAction("Index");
            }
            return View(empVm);
        }

        [Route("Employee/EditProfile")]
        public IActionResult EditEmployee()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var empVm = _empService.GetEmployeeForEdit(userId);
            if(empVm == null)
            {
                _logger.LogInformation("Can't edit employee - employee dosen't exist");
                return RedirectToAction("Index");
            }
            _empService.SetParametersToVm(empVm);
            return View(empVm);
        }

        [HttpPost]
        [Route("Employee/EditProfile")]
        public IActionResult EditEmployee(NewEmployeeVm empVm)
        {
            if (!ModelState.IsValid)
            {
                _empService.SetParametersToVm(empVm);
                return View(empVm.Id);
            }
            _empService.UpdateEmployee(empVm);
            return RedirectToAction("ViewEmployee");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _empService.DeleteEmployee(id);
            _logger.LogInformation("Employee " + id +" has been deleted");
            return RedirectToAction("Index");
        }

        /****Vehicle****/

        public IActionResult NewVehicle()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = new NewVehicleVm
            {
                EmployeeId = _empService.GetEmployeeByUserId(userId).Id,
                EngineTypes = _empService.GetEngineTypes().ToList()
            };
            return PartialView("AddNewVehicleForEmployee", model);
        }

        [HttpPost]
        public IActionResult AddNewVehicleForEmployee(NewVehicleVm vehVm)
        {
            if (!ModelState.IsValid)
            {
                vehVm.EngineTypes = _empService.GetEngineTypes().ToList();
                return RedirectToAction("NewVehicle");
            }
            var id = _empService.AddVehicle(vehVm);
            return RedirectToAction("EditEmployee");
        }

        [ServiceFilter(typeof(CheckVehiclePermission))]
        public IActionResult EditVehicle(int id)
        {
            var veh = _empService.GetVehicleForEdit(id);
            if(veh == null)
            {
                _logger.LogInformation("Can't edit vehicle - vehicle dosen't exist");
                return RedirectToAction("EditEmployee");
            }
            veh.EngineTypes = _empService.GetEngineTypes().ToList();
            return PartialView("EditVehicleForEmployee", veh);
        }

        [HttpPost]
        public IActionResult EditVehicleForEmployee(NewVehicleVm vehVm)
        {
            _empService.UpdateVehicle(vehVm);
            return RedirectToAction("EditEmployee");
        }

        [ServiceFilter(typeof(CheckVehiclePermission))]
        public IActionResult DeleteVehicle(int id)
        {
            _empService.DeleteVehicle(id);
            return RedirectToAction("EditEmployee");
        }

        /****Contact****/

        public IActionResult NewContact()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = new NewContactDetailsVm
            {
                EmployeeId = _empService.GetEmployeeByUserId(userId).Id,
            ContactDetailTypes = _empService.GetConactDetailTypes().ToList()
            };
            return PartialView("AddNewContactForEmployee", model);
        }

        [HttpPost]
        public IActionResult AddNewContactForEmployee(NewContactDetailsVm conVm)
        {
            var id = _empService.AddContact(conVm);
            return RedirectToAction("EditEmployee");
        }

        [ServiceFilter(typeof(CheckContactPermission))]
        public IActionResult EditContact(int id)
        {
            var model = _empService.GetContactForEdit(id);
            if(model == null)
            {
                _logger.LogInformation("Can't edit contact - contact dosen't exist");
                return RedirectToAction("EditEmployee");
            }
            model.ContactDetailTypes = _empService.GetConactDetailTypes().ToList();
            return PartialView("EditContactForEmployee", model);
        }

        [HttpPost]
        public IActionResult EditContactForEmployee(NewContactDetailsVm con)
        {
            _empService.UpdateContact(con);
            return RedirectToAction("EditEmployee");

        }

        [ServiceFilter(typeof(CheckContactPermission))]
        public IActionResult DeleteContact(int id)
        {
            _empService.DeleteContact(id);
            return RedirectToAction("EditEmployee");
        }
    }
}
