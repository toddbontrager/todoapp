using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    // route template
    [Route("api/[controller]")]
    public class ToDosController : Controller
    {
        private IToDoRepository _toDoRepository;

        public ToDosController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        // GET api/todos
        [HttpGet]
        public IActionResult GetTodos()
        {
            // return Ok(ToDoDataStore.Current.ToDos);
            var toDoEntities = _toDoRepository.GetToDos();
            var results = new List<ToDoDto>();

            foreach (var toDoEntity in toDoEntities)
            {
                results.Add(new ToDoDto
                {
                    Id = toDoEntity.Id,
                    Task = toDoEntity.Task
                });
            }
            return Ok(results);
        }

        // GET api/todos/2
        [HttpGet("{id}")]
        public IActionResult GetToDo(int id)
        {
            var toDoToReturn = _toDoRepository.GetToDo(id);

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
        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            var toDoFromStore = ToDoDataStore.Current.ToDos.FirstOrDefault(t => t.Id == id);
            if (toDoFromStore == null)
            {
                return NotFound();
            }

            ToDoDataStore.Current.ToDos.Remove(toDoFromStore);
            return NoContent();
        }
    }
}