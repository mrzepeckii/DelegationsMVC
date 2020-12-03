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
            _userService.GetAllUsers();
            return View();
        }
    }
}
