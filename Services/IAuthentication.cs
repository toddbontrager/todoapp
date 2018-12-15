using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface IAuthentication
    {
        UserDto Authenticate(LoginDto login);
        string BuildToken(UserDto user);
        int FindUserAge(ClaimsPrincipal user);
    }
}
