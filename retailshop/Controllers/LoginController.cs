using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using retailshop.Models;
using retailshop.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private AppDbContext _appDbContext;
        public LoginController(IConfiguration config,AppDbContext appDbContext)
        {
            _config = config;
            _appDbContext = appDbContext;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/Login")]
        public IActionResult Login( string username,string password)
        {
            //IActionResult response = Unauthorized();
            var user = AuthenticateUser(username, password);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                return  Ok(new { token = tokenString });
            }

            return NoContent();
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Name, userInfo.Username),
        new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
        new Claim(JwtRegisteredClaimNames.Sub,userInfo.Password),
        new Claim("DateOfJoing", userInfo.DateOfJoing),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_config["JwtSettings:Issuer"],
                _config["JwtSettings:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(string username,string password)
        {
            UserModel user = null;
            UserModel users =(UserModel) _appDbContext.UserModel.Where(lo=>lo.Username==username).Select(pro=>pro).Single();
            if (users.Password != password) return null;
            if (users!=null)
            {
                user = new UserModel { Username = users.Username, EmailAddress = users.EmailAddress,DateOfJoing=users.DateOfJoing,Password=users.Password };
            }
            return user;
        }
    }
}