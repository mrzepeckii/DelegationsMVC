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
    public class CheckEmpPermission : Attribute, IAuthorizationFilter
    {
        private  IEmployeeService _empService;
        public CheckEmpPermission(IEmployeeService empService)
        {
            _empService = empService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var x = context.HttpContext.Request;
            bool isAuthorized = CheckUserPermission(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                context.HttpContext.Request.RouteValues["id"].ToString());

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(string userId, string stringValues)
        {            
            var id = _empService.GetEmployeeByUserId(userId).Id;
            return id.ToString() == stringValues;
        }
    }
}
