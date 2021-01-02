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
    public class CheckEmployeeDelegationPermission : Attribute, IAuthorizationFilter
    {
        private readonly IDelegationService _delegService;
        private readonly IEmployeeService _empService;

        public CheckEmployeeDelegationPermission(IDelegationService delegService, IEmployeeService empService)
        {
            _delegService = delegService;
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

        private bool CheckUserPermission(string userId, string id)
        {
            var emp = _empService.GetEmployeeByUserId(userId);
            return emp.Id.ToString() == id;
        }
    }
}
