using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using Microsoft.AspNetCore.Mvc;

namespace DelegationsMVC.Web.Controllers
{
    public class DelegationController : Controller
    {
        private readonly IDelegationService _delegService;
        private readonly IEmployeeService _empService;

        public DelegationController(IDelegationService delegService, IEmployeeService empService)
        {
            _delegService = delegService;
            _empService = empService;
        }
        public IActionResult Index()
        {
            var delegations = _delegService.GetAllDelegationsForListByStatus(1);
            return View(delegations);
        }

        [HttpGet]
        public IActionResult AddDelegation(int id)
        {
            var emp = _empService.GetEmployeeById(id);
            if(emp == null)
            {
                return RedirectToAction("Index");
            }
            var model = new NewDelegationVm()
            {
                EmployeeId = emp.Id,
            };
            model = _delegService.SetParametersToVm(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDelegation(NewDelegationVm delVm)
        {
            if (!ModelState.IsValid)
            {
                if( delVm.Routes.Count != 3)
                {
                    delVm.Routes.Add(new NewRouteVm());
                }
                delVm = _delegService.SetParametersToVm(delVm);
                return View(delVm);
            }
            var id = _delegService.AddDelegation(delVm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditDelegation(int id)
        {
            var del = _delegService.GetDelegationForEdit(id);
            if(del == null)
            {
                return RedirectToAction("Index");
            }
            _delegService.SetParametersToVm(del);
            return View(del);
        }

        [HttpPost]
        public IActionResult EditDelegation(NewDelegationVm del)
        {
            if (!ModelState.IsValid)
            {
                _delegService.SetParametersToVm(del);
                return View(del);
            }
            _delegService.UpdateDelegation(del);
            return RedirectToAction("Index");

        }
        //[HttpGet]
        //public IActionResult AddNewRouteForDelegation(int id)
        //{
        //    var del = _delegService.GetDelegationById(id);
        //    if(del == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    var model = new NewRouteVm()
        //    {
        //        DelegationId = id,
        //        RouteDetail = new NewRouteDetailVm()
        //    };
        //    model = _delegService.SetParametersToVm(model);
        //    return View(model);
        //}

        public IActionResult NewRoute(int id)
        {
            var del = _delegService.GetDelegationById(id);
            if (del == null)
            {
                return RedirectToAction("Index");
            }

            var model = new NewRouteVm()
            {
                DelegationId = id,
                RouteDetail = new NewRouteDetailVm()
            };
            model = _delegService.SetParametersToVm(model);
            return PartialView("AddNewRouteForDelegation", model);
        }

        [HttpPost]
        public IActionResult AddNewRouteForDelegation(NewRouteVm routeVm)
        {
            if (!ModelState.IsValid)
            {
                _delegService.SetParametersToVm(routeVm);
                return View(routeVm);
            }
            var id = _delegService.AddRoute(routeVm);
            return RedirectToAction("EditDelegation", new { id = routeVm.DelegationId });
        }

        public IActionResult DeleteRoute(int idRoute)
        {
            _delegService.DeleteRoute(idRoute);
        }
    }
}
