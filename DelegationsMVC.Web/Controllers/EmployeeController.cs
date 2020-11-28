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

        [Authorize( Roles = "Accounant, Chief, Admin")]
        public IActionResult Index()
        {
            var employees = _empService.GetAllEmployeeForList();
            return View(employees);
        }

        /****Employe****/
        // [HttpGet]
        [Route("Employee/AddProfile")]
        public IActionResult AddEmployee()
        {
            var model = new NewEmployeeVm()
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            _empService.SetParametersToVm(model);
            return View(model);
        }

        [HttpPost]
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
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Authorize(Roles = "Accounant, Chief, Admin")]
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
            var emp = _empService.GetEmployeeByUserId(userId);
            var empVm = _empService.GetEmployeeDetails(emp.Id);
            if(empVm == null)
            {
                _logger.LogInformation("Can't show employee details - employee dosen't exist");
                return RedirectToAction("Index");
            }
            return View(empVm);
        }

        // [HttpGet]
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
        public IActionResult EditEmployee(NewEmployeeVm empVm)
        {
            if (!ModelState.IsValid)
            {
                _empService.SetParametersToVm(empVm);
                return View(empVm.Id);
            }
            _empService.UpdateEmployee(empVm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _empService.DeleteEmployee(id);
            _logger.LogInformation("Employee " + id +" has been deleted");
            return RedirectToAction("Index");
        }

        /****Vehicle****/

        public IActionResult NewVehicle(int id)
        {
            var model = new NewVehicleVm
            {
                EmployeeId = id,
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
                return RedirectToAction("NewVehicle", new { id = vehVm.EmployeeId });
            }
            var id = _empService.AddVehicle(vehVm);
            return RedirectToAction("EditEmployee", new { id = vehVm.EmployeeId });
        }

        [ServiceFilter(typeof(CheckEmpPermission))]
        public IActionResult EditVehicle(int id)
        {
            var veh = _empService.GetVehicleForEdit(id);
            if(veh == null)
            {
                _logger.LogInformation("Can't edit vehicle - vehicle dosen't exist");
                return RedirectToAction("EditEmployee", new { id = veh.EmployeeId });
            }
            veh.EngineTypes = _empService.GetEngineTypes().ToList();
            return PartialView("EditVehicleForEmployee", veh);
        }

        [HttpPost]
        public IActionResult EditVehicleForEmployee(NewVehicleVm vehVm)
        {
            _empService.UpdateVehicle(vehVm);
            return RedirectToAction("EditEmployee", new { id = vehVm.EmployeeId });
        }

        [ServiceFilter(typeof(CheckEmpPermission))]
        public IActionResult DeleteVehicle(int idVeh, int idEmp)
        {
            _empService.DeleteVehicle(idVeh);
            return RedirectToAction("EditEmployee", new { id = idEmp });
        }

        /****Contact****/

        public IActionResult NewContact(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var emp = _empService.GetEmployeeByUserId(userId);
            if (emp.Id != id)
            {
                return RedirectToAction("Index");
            }
            var model = new NewContactDetailsVm
            {
                EmployeeId = id,
                ContactDetailTypes = _empService.GetConactDetailTypes().ToList()
            };
            return PartialView("AddNewContactForEmployee", model);
        }

        [HttpPost]
        public IActionResult AddNewContactForEmployee(NewContactDetailsVm conVm)
        {
            var id = _empService.AddContact(conVm);
            return RedirectToAction("EditEmployee", new { id = conVm.EmployeeId });
        }

        [ServiceFilter(typeof(CheckEmpPermission))]
        public IActionResult EditContact(int id)
        {
            var model = _empService.GetContactForEdit(id);
            if(model == null)
            {
                _logger.LogInformation("Can't edit contact - contact dosen't exist");
                return RedirectToAction("EditEmployee", new { id = model.EmployeeId });
            }
            model.ContactDetailTypes = _empService.GetConactDetailTypes().ToList();
            return PartialView("EditContactForEmployee", model);
        }

        [HttpPost]
        public IActionResult EditContactForEmployee(NewContactDetailsVm con)
        {
            _empService.UpdateContact(con);
            return RedirectToAction("EditEmployee", new { id = con.EmployeeId });

        }

        [ServiceFilter(typeof(CheckEmpPermission))]
        public IActionResult DeleteContact(int idCon, int idEmp)
        {
            _empService.DeleteContact(idCon);
            return RedirectToAction("EditEmployee", new { id = idEmp });
        }
    }
}
