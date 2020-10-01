using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult AddEmployee()
        {
            //ViewBag.empTypes = _empService.GetEmployeeTypes();
            //ViewBag.contactTypes = _empService.GetConactDetailTypes();
            //ViewBag.vehicleTypes = _empService.GetEngineTypes();
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
            //model.ContactDetails = new List<NewContactDetailsVm>() { new NewContactDetailsVm() { ContactDetailInformation = "333321", ContactDetailTypeId = 2 } };
            var id = _empService.AddEmployee(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewEmployee(int id)
        {
            var emp = _empService.GetEmployeeDetails(id);
            return View(emp);
        }

        public IActionResult Delete(int id)
        {
            _empService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddNewEmailForEmployee(int employeeId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewPhoneForEmployee(int employeeId)
        {
            return View();
        }
    }
}
