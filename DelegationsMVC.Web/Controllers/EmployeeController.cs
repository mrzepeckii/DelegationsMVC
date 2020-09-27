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
            var empTypes = _empService.GetEmployeeTypes();
            ViewBag.empTypes = empTypes;
            //pusty formularz do wypelnienia
            return View(new NewEmployeeVm());
        }

        [HttpPost]
        public IActionResult AddEmployee(NewEmployeeVm model)
        {
            var id = _empService.AddEmployee(model);
            return Redirect("Index");
        }

        public IActionResult ViewEmployee(int employeeId)
        {
            return View();
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
