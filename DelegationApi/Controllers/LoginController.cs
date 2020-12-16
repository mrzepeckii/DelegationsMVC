using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DelegationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            IActionResult result = Unauthorized();
            var success = AuthentiacteUser(userModel);
            if (success)
            {
                var tokenStr = GenerateJsonWebToken(userModel);
                result = Ok(new { token = tokenStr });
            }
            return result;
        }

        private object GenerateJsonWebToken(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        private bool AuthentiacteUser(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
