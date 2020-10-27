using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
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
    }
}
