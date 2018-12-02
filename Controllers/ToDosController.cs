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
        public IActionResult GetTodos()
        {
            return Ok(ToDoDataStore.Current.ToDos);
        }

        // GET api/todos/2
        [HttpGet("{id}")]
        public IActionResult GetToDo(int id)
        {
            var toDoToReturn = ToDoDataStore.Current.ToDos.FirstOrDefault(t => t.Id == id);
            if (toDoToReturn == null)
            {
                return NotFound();
            }
            return Ok(toDoToReturn);
        }


        // POST


        // DELETE
    }
}