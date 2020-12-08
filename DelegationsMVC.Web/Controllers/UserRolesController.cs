using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DelegationsMVC.Application.Interfaces;
using DelegationsMVC.Application.ViewModels.UserVm;
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
            return View(model);
        }

        //[HttpGet]
        public IActionResult AddRolesToUser(string id)
        {
            var userVm = _userService.GetUserDetails(id);
            return PartialView("AddRolesToUser", userVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddRolesToUser(UserDetailVm user)
        {
            await _userService.AddRolesToUser(user.Id, user.UserRoles);
            return RedirectToAction("Index");
        }
    }
}
