using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DelegationsMVC.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _empService;
        public EmployeeController(IEmployeeService empService)
        {
            _empService = empService;
        }

        public IActionResult Index()
        {
            var employees = _empService.GetAllEmployeeForList();
            return View(employees);
        }

        public IActionResult GetEngineTypes()
        {
            var engineTypes = _empService.GetEngineTypes().ToList();
            return Json(new SelectList(engineTypes, "Id", "Name"));
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            var model = new NewEmployeeVm()
            {
                EmployeeTypes = _empService.GetEmployeeTypes().ToList(),
                ContactDetailTypes = _empService.GetConactDetailTypes().ToList(),
                EngineTypes = _empService.GetEngineTypes().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddEmployee(NewEmployeeVm model)
        {
            var id = _empService.AddEmployee(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewEmployee(int id)
        {
            var emp = _empService.GetEmployeeDetails(id);
            return View(emp);
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var emp = _empService.GetEmployeeForEdit(id);
            emp.EngineTypes = _empService.GetEngineTypes().ToList();
            emp.ContactDetailTypes = _empService.GetConactDetailTypes().ToList();
            emp.EmployeeTypes = _empService.GetEmployeeTypes().ToList();
            return View(emp);
        }

        [HttpPost]
        public IActionResult EditEmployee(NewEmployeeVm empVm)
        {
            _empService.UpdateEmployee(empVm);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _empService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteVehicle(int idVeh, int idEmp)
        {
            _empService.DeleteVehicle(idVeh);
            return RedirectToAction("EditEmployee", new { id = idEmp });
        }

        public IActionResult DeleteContact(int idCon, int idEmp)
        {
            _empService.DeleteContact(idCon);
            return RedirectToAction("EditEmployee", new { id = idEmp });
        }

        [HttpGet]
        public IActionResult AddNewVehicleForEmployee(int employeeId)
        {
            var model = new NewVehicleVm
            {
                EmployeeId = employeeId,
                EngineTypes = _empService.GetEngineTypes().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddNewPhoneForEmployee(NewVehicleVm vehVm)
        {
            _empService.AddVehicle(vehVm);
            return RedirectToAction("EditEmployee", new { id = vehVm.EmployeeId });
        }
    }
}
