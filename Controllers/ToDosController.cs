using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoApp.Models;
using ToDoApp.Validators;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    // route template
    [Route("api/[controller]")]
    public class ToDosController : Controller
    {
        private IToDoService _toDoService;

        public ToDosController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET api/todos
        [HttpGet, Authorize]
        public IActionResult GetTodos()
        {
           var toDos = _toDoService.GetAllToDos(HttpContext);        
           return Ok(toDos);
        }

        // GET api/todos/2
        [HttpGet("{id}", Name ="GetToDo")]
        [ServiceFilter(typeof(ValidateEntityExistsFilter))]
        public IActionResult GetToDo(int id)
        {
            var result = _toDoService.GetToDoById(id);
            return Ok(result);
        }

        // POST api/todos
        [HttpPost]
        [ServiceFilter(typeof(ValidateRequestBodyFilter))]
        public IActionResult CreateToDo([FromBody] ToDoDto toDo)
        {
            var results = _toDoService.CreateToDo(toDo);
            return CreatedAtRoute("GetToDo", new { id = results.Id }, results);
        }

        // PUT api/todos/2
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateRequestBodyFilter))]
        [ServiceFilter(typeof(ValidateEntityExistsFilter))]
        public IActionResult UpdateToDo(int id, [FromBody] ToDoDto toDo)
        {
            _toDoService.UpdateToDo(id, toDo);
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsFilter))]
        public IActionResult DeleteToDo(int id)
        {
            _toDoService.DeleteToDo(id);
            return NoContent();
        }
    }
}