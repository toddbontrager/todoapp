using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using ToDoApp.Models;

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

        public int FindUserAge(ClaimsPrincipal currentUser)
        {
            var userAge = 0;
            if (UserHasClaim(currentUser))
            {
                DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
                userAge = DateTime.Today.Year - birthDate.Year;
            }
            return userAge;
        }

        private bool UserHasClaim(ClaimsPrincipal currentUser)
        {
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return true;
            }
            return false;
        }
    }
}
