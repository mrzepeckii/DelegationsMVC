using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
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
            //pusty formularz do wypelnienia
            return View();
        }

        //[HttpPost]
        //public IActionResult AddEmployee()
        //{
        //    //
        //    return View();
        //}

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
