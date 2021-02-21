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
    public class CheckRoutePermission : Attribute, IAuthorizationFilter
    {
        private readonly IDelegationService _delegService;
        private readonly IEmployeeService _empService;
        public CheckRoutePermission(IDelegationService delegService, IEmployeeService empService)
        {
            _delegService = delegService;
            _empService = empService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermission(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
               context.HttpContext.Request.RouteValues["idRoute"].ToString());

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(string userId, string id)
        {
            var emp = _empService.GetEmployeeByUserId(userId);
            var route = _delegService.GetRouteById(Int32.Parse(id));
            var del = _delegService.GetDelegationById(route.DelegationId);
            return del.EmployeeId == emp.Id;
        }
    }
}
