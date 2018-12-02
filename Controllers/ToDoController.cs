using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    [Route("api/todos")]
    
    public class ToDoController : Controller
    {
        // GET api/todos
        [HttpGet()]
        public JsonResult GetTodos()
        {
            return new JsonResult(ToDoDataStore.Current.ToDos);
        }

        // GET api/todos/3
    }
}