using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController :Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public TokenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginDto login)
        {
            // TODO: Refactor out of controller and into service
            IActionResult response = Unauthorized();
            var user = _authenticationService.Authenticate(login);

            if (user != null)
            {
                var tokenString = _authenticationService.BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}
