using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

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

        // POST api/todos
        [HttpPost]
        public IActionResult CreateToDo(int id, [FromBody] ToDoDto toDo)
        {
            if (toDo == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ToDoDataStore.Current.ToDos.Add(toDo);
            return StatusCode(201);
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, [FromBody] ToDoDto toDo)
        {
            if (toDo == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDoFromStore = ToDoDataStore.Current.ToDos.FirstOrDefault(t => t.Id == id);
            if (toDoFromStore == null)
            {
                return NotFound();
            }

            toDoFromStore.Task = toDo.Task;
            return NoContent();
        }


        // DELETE
    }
}