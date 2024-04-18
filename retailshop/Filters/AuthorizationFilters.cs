
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using retailshop.Repository;
using Microsoft.Extensions.Logging;
using retailshop.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
namespace retailshop.Filters
{
    public class AuthorizationFilters : Attribute, IAuthorizationFilter
    {
        public readonly IUserCheck userCheck;
        public AuthorizationFilters(IUserCheck userCheck) {
            this.userCheck = userCheck;    
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuth = AuthorizationLogic(context.HttpContext);
            if (!isAuth) { context.Result = new UnauthorizedObjectResult("UnAuthorized"); }
        }


        private bool AuthorizationLogic(HttpContext context)
        {
            var userName = context.User.Claims.FirstOrDefault().Value;
            var userMail=context.User.FindFirstValue(ClaimTypes.Email);
            var userpassword = context.User.Claims.First(c => c.Type == "Sub").Value;

            if (userCheck.CheckUser(userName,userMail)) return true;
            return false;
        }

    }
}
