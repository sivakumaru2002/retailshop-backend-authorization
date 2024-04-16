
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using retailshop.Repository;
using Microsoft.Extensions.Logging;
using retailshop.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
namespace retailshop.Filters
{
    public class AuthorizationFilters : Attribute, IAuthorizationFilter
    {

        public void resource(HttpContext httpContextAccessor)
        {
            var userService = httpContextAccessor.RequestServices.GetRequiredService(typeof(IUserCheck)) as IUserCheck;
            var user = httpContextAccessor.User;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuth = AuthorizationLogic(context.HttpContext.User);
            if (!isAuth) { context.Result = new UnauthorizedObjectResult("UnAuthorized"); }
        }


        private bool AuthorizationLogic(System.Security.Claims.ClaimsPrincipal user)
        {
            if (user.Identity.Name=="Srinidhi") return true;
            return false;
        }

    }
}
