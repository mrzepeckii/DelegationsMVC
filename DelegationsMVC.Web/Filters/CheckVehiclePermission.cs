using DelegationsMVC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DelegationsMVC.Web.Filters
{
    public class CheckVehiclePermission : Attribute, IAuthorizationFilter
    {
        private readonly IEmployeeService _empService;
        public CheckVehiclePermission(IEmployeeService empService)
        {
            _empService = empService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermission(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
              context.HttpContext.Request.RouteValues["id"].ToString());
            
            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(string userId, string stringValues)
        {
            var id = _empService.GetVehicleById(Int32.Parse(stringValues)).EmployeeId;
            var emp = _empService.GetEmployeeById(id);
            var empUser = _empService.GetEmployeeByUserId(userId);
            return emp.Id == empUser.Id;
        }

    }
}
