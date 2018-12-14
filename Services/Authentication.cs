using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace ToDoApp.Services
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _config;

        public Authentication(IConfiguration config)
        {
            _config = config;
        }

        public UserDto Authenticate(LoginDto login)
        {
            UserDto user = null;

            if (login.Username == "todd" && login.Password == "secret")
            {
                user = new UserDto
                {
                    Name = "Todd B",
                    Email = "test@gmail.com",
                    Birthdate = new DateTime(1980,1,1)
                };
            }

            return user;
        }

        public string BuildToken(UserDto user)
        {

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
