using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private IAuthentication _authentication;

        public TokenController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginDto login)
        {
            IActionResult response = Unauthorized();
            var user = _authentication.Authenticate(login);

            if (user != null)
            {
                var tokenString = _authentication.BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        //private string BuildToken(UserDto user)
        //{
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //    _config["Jwt:Issuer"],
        //     expires: DateTime.Now.AddMinutes(30),
        //    signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}