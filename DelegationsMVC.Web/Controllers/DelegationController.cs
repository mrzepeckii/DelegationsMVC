using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using DelegationsMVC.Web.Filters;
using DelegationsMVC.Web.Helpers;
using IronPdf;

namespace DelegationsMVC.Web.Controllers
{
    [Authorize]
    public class DelegationController : Controller
    {
        private readonly IDelegationService _delegService;
        private readonly IEmployeeService _empService;
        private readonly ILogger<DelegationController> _logger;
        private readonly IHostEnvironment _host;

        public DelegationController(IDelegationService delegService, IEmployeeService empService, ILogger<DelegationController> logger, IHostEnvironment host)
        {
            _delegService = delegService;
            _empService = empService;
            _logger = logger;
            _host = host;
        }

        [Route("Delegation/All")]
        [Authorize(Roles = "Chief, Accountant, Admin")]
        public IActionResult Index()
        {
            
            var delegations = _delegService.GetAllDelegationsForList();
            return View(delegations);
        }

        [Route("Delegation/All/{id}")]
        [ServiceFilter(typeof(CheckEmployeeDelegationPermission))]
        public IActionResult Index(int id)
        {
            var delegations = _delegService.GetDelegationsByEmployee(id);
            return View(delegations);
        }

        [Route("Delegation/New")]
        public IActionResult AddDelegation()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if( _empService.CheckIfEmployeeExist(userId) )
            {
                _logger.LogInformation("Can't add delegation - employee dosen't exist");
                return RedirectToAction("AddEmployee", "Employee");
            }
            var model = new NewDelegationVm()
            {
                EmployeeId = _empService.GetEmployeeByUserId(userId).Id
            };
            model = _delegService.SetParametersToVm(model);
            return View(model);
        }

        [HttpPost]
        [Route("Delegation/New")]
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
        [ServiceFilter(typeof(CheckDelegationPermission))]
        [Route("Delegation/Edit/{id}")]
        public IActionResult EditDelegation(int id)
        {
            var isEditable = _delegService.IsDelegationEditableById(id);
            if (!isEditable)
            {
                return RedirectToAction("ViewDelegation", new { id });
            }
            var del = _delegService.GetDelegationForEdit(id);
            if(del == null)
            {
                _logger.LogInformation("Can't edit delegation - delegation dosen't exist");
                return RedirectToAction("Index");
            }
            _delegService.SetParametersToVm(del);
            return View(del);
        }

        [HttpPost]
        [Route("Delegation/Edit/{id}")]
        public IActionResult EditDelegation(NewDelegationVm del)
        {
            if (!ModelState.IsValid)
            {
                _delegService.SetParametersToVm(del);
                return View(del);
            }
            _delegService.UpdateDelegation(del);
            return RedirectToAction("ViewDelegation", new { id = del.Id });

        }

        [ServiceFilter(typeof(CheckDelegationPermission))]
        public IActionResult DeleteDelegation(int id)
        {
            _delegService.CancelDelegation(id);
            _logger.LogInformation("Delegation " + id + " - has been deleted");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Delegation/View/{id}")]
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

        [ServiceFilter(typeof(CheckDelegationPermission))]
        public IActionResult NewRoute(int id)
        {
            var isEditable = _delegService.IsDelegationEditableById(id);
            var del = _delegService.GetDelegationById(id);
            if (del == null && !isEditable)
            {
                _logger.LogInformation("Can't add new route to the delegation - delegation dosen't exist or is closed");
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

        [ServiceFilter(typeof(CheckRoutePermission))]
        public IActionResult DeleteRoute(int idRoute, int idDel)
        {
            var isEditable = _delegService.IsDelegationEditableById(idDel);
            if (!isEditable)
            {
                return RedirectToAction("ViewDelegation", new { id = idDel }); ;
            }
            _delegService.DeleteRoute(idRoute);
            return RedirectToAction("EditDelegation", new { id = idDel });
        }

        [ServiceFilter(typeof(CheckRoutePermission))]
        public IActionResult EditRoute(int idRoute, int idDel)
        {
            var isEditable = _delegService.IsDelegationEditableById(idDel);
            if (!isEditable)
            {
                return RedirectToAction("ViewDelegation", new { id = idDel });
            }
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

        [Authorize(Roles = "Chief, Accountant, Admin")]
        public IActionResult AcceptOrPaidDelegation(int delId, int delStatus)
        {
            var isChanged =_delegService.ChangeStatusOfDelegation(delId, delStatus);
            if (!isChanged)
            {
                _logger.LogInformation("Can't change status to Oplacona - accountant acceptance is required/delegation dosen't exists/has demanded status");
                return RedirectToAction("ViewDelegation", new { id = delId });
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Chief, Accountant, Admin")]
        public IActionResult GenerateDelegationReport(int id)
        {
            var model = _delegService.GetDelegationDetails(id);
            Installation.TempFolderPath = $@"{_host.ContentRootPath}/irontemp/";
            Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var html = this.RenderViewAsync("ReportDelegation", model, true);
            var ironPdfRender = new HtmlToPdf();
            ironPdfRender.PrintOptions.CustomCssUrl = new Uri("https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.css").ToString();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            return File(pdfDoc.Stream.ToArray(), "application/pdf");
        }
    }
}
