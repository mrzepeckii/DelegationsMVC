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
                Destinations = _delegService.GetAllDestinations().ToList(),
                RouteTypes = _delegService.GetRouteTypes().ToList(),
                TransportTypes = _delegService.GetTransportTypes().ToList(),
                Vehicles = _empService.GetVehiclesByEmploee(id).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDelegation(NewDelegationVm delVm)
        {
            var id = _delegService.AddDelegation(delVm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddNewRouteForDelegation(int id)
        {
            var del = _delegService.GetDelegationById(id);
            if(del == null)
            {
                return RedirectToAction("Index");
            }

            var modelDetail = new NewRouteDetailVm()
            {
                Vehicles = _empService.GetVehiclesByEmploee(del.EmployeeId).ToList(),
            };
            var model = new NewRouteVm()
            {
                DelegationId = id,
                RouteDetail = modelDetail,
                RouteTypes = _delegService.GetRouteTypes().ToList(),
                TransportTypes = _delegService.GetTransportTypes().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddNewRouteForDelegation(NewRouteVm routeVm)
        {
            var id = _delegService.AddRoute(routeVm);
            return RedirectToAction("Index");
        }
    }
}
