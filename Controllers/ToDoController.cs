using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    
    public class ToDoController : Controller
    {
        // GET api/todos
        [HttpGet]
        public IActionResult GetTodos()
            return Ok()

        // GET api/todos/3
    }
}