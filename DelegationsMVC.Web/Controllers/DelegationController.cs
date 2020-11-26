using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DelegationsMVC.Web.Controllers
{
    public class DelegationController : Controller
    {
        private readonly IDelegationService _delegService;
        private readonly IEmployeeService _empService;
        private readonly ILogger<DelegationController> _logger;

        public DelegationController(IDelegationService delegService, IEmployeeService empService, ILogger<DelegationController> logger)
        {
            _delegService = delegService;
            _empService = empService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            //var delegations = _delegService.GetAllDelegationsForListByStatus(1);
            var delegations = _delegService.GetAllDelegationsForList();
            return View(delegations);
        }

        //[HttpGet]
        public IActionResult AddDelegation()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var emp = _empService.GetEmployeeByUserId(userId);
            if(emp == null)
            {
                _logger.LogInformation("Can't add delegation - employee dosen't exist");
                return RedirectToAction("AddEmployee", "Employee");
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var emp = _empService.GetEmployeeByUserId(userId);
            var del = _delegService.GetDelegationForEdit(id);
            if(del == null || del.EmployeeId != emp.Id)
            {
                _logger.LogInformation("Can't edit delegation - delegation dosen't exist or user have no rights to edit this delegation");
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

        public IActionResult DeleteDelegation(int id)
        {
            _delegService.CancelDelegation(id);
            _logger.LogInformation("Delegation" + id + " - has been deleted");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewDelegation(int id)
        {
            var del = _delegService.GetDelegationDetails(id);
            if(del == null)
            {
                _logger.LogInformation("Can't show delegation - delegation dosen't exist");
                return RedirectToAction("Index");
            }
            return View(del);
        }

        public IActionResult NewRoute(int id)
        {
            var del = _delegService.GetDelegationById(id);
            if (del == null)
            {
                _logger.LogInformation("Can't add new route to the delegation - delegation dosen't exist");
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

        public IActionResult DeleteRoute(int idRoute, int idDel)
        {
            _delegService.DeleteRoute(idRoute);
            return RedirectToAction("EditDelegation", new { id = idDel });
        }

        public IActionResult EditRoute(int idRoute, int idDel)
        {
            var model = _delegService.GetRouteForEdit(idRoute);
            if(model == null)
            {
                _logger.LogInformation("Can't edit route - route dosen't exist");
                return RedirectToAction("EditDelegation", new { id = idDel });
            }
            _delegService.SetParametersToVm(model);
            return PartialView("EditRouteForDelegation", model);
        }

        [HttpPost]
        public IActionResult EditRouteForDelegation(NewRouteVm routeVm)
        {
            if (!ModelState.IsValid)
            {
                _delegService.SetParametersToVm(routeVm);
                return View(routeVm);
            }
            _delegService.UpdateRoute(routeVm);
            return RedirectToAction("EditDelegation", new { id = routeVm.DelegationId });
        }

        public IActionResult AcceptOrPaidDelegation(int delId, int delStatus)
        {
            var isChanged =_delegService.ChangeStatusOfDelegation(delId, delStatus);
            if (!isChanged)
            {
                _logger.LogInformation("Can't change status to Oplacona - accountant acceptance is required or delegation dosen't exists");
                return RedirectToAction("ViewDelegation", new { id = delId });
            }
            return RedirectToAction("Index");
        }
    }
}
