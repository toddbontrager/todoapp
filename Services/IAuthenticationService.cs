using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface IAuthenticationService
    {
        UserDto Authenticate(LoginDto login);
        string BuildToken(UserDto user);
    }
}
