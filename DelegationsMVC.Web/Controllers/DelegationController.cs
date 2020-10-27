using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using Microsoft.AspNetCore.Mvc;

namespace DelegationsMVC.Web.Controllers
{
    public class DelegationController : Controller
    {
        private readonly IDelegationService _delegService;

        public DelegationController(IDelegationService delegService)
        {
            _delegService = delegService;
        }
        public IActionResult Index()
        {
            var delegations = _delegService.GetAllDelegationsForListByStatus(1);
            return View(delegations);
        }

        [HttpGet]
        public IActionResult AddDelegation()
        {
            var model = new NewDelegationVm()
            {
                Destinations = _delegService.GetAllDestinations().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDelegation(NewDelegationVm delVm)
        {
            delVm.DelegationStatusId = 1;
            var id = _delegService.AddDelegation(delVm);
            return RedirectToAction("Index");
        }
    }
}
