using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}