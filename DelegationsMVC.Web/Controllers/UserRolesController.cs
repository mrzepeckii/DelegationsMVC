using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DelegationsMVC.Web.Controllers
{
    [Authorize (Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly IUserService _userService;
        public UserRolesController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("Users/All")]
        public IActionResult Index()
        {
            var model = _userService.GetAllUsers();
            var model2 = _userService.GetUserDetails("7bdb4634-203c-4872-8ae5-6b86f8cadbfb");
            return View(model);
        }
    }
}
