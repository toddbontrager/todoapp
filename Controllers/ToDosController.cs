using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    // route template
    [Route("api/[controller]")]
    public class ToDosController : Controller
    {
        // GET api/todos
        [HttpGet]
        public JsonResult GetTodos()
        {
            return new JsonResult(ToDoDataStore.Current.ToDos);
        }

        // GET api/todos/2
        [HttpGet("{id}")]
        public JsonResult GetToDo(int id)
        {
            return new JsonResult(ToDoDataStore.Current.ToDos.FirstOrDefault(t => t.Id == id));
        }


        // POST


        // DELETE
    }
}