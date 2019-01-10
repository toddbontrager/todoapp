using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoApp.Models;
using ToDoApp.Repository;
using ToDoApp.Validators;

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
            var toDoEntities = _toDoRepository.GetToDos();
            var results = Mapper.Map<IEnumerable<ToDoDto>>(toDoEntities);            
            return Ok(results);
        }

        // GET api/todos/2
        [HttpGet("{id}", Name ="GetToDo")]
        [ServiceFilter(typeof(ValidateEntityExistsFilter))]
        public IActionResult GetToDo(int id)
        {
            var result = _toDoRepository.GetToDo(id);
            return Ok(result);
        }

        // POST api/todos
        [HttpPost]
        [ServiceFilter(typeof(ValidateRequestBodyFilter))]
        public IActionResult CreateToDo([FromBody] ToDoDto toDo)
        {
            var mapped = Mapper.Map<Entities.ToDo>(toDo);
            _toDoRepository.AddToDo(mapped);

            if (!_toDoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdToDoToReturn = Mapper.Map<ToDoDto>(mapped);

            return CreatedAtRoute("GetToDo", new { id = createdToDoToReturn.Id }, createdToDoToReturn);
        }

        // PUT api/todos/2
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidateRequestBodyFilter))]
        [ServiceFilter(typeof(ValidateEntityExistsFilter))]
        public IActionResult UpdateToDo(int id, [FromBody] ToDoDto toDo)
        {
            var result = _toDoRepository.GetToDo(id);
            Mapper.Map(toDo, result);

            if (!_toDoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsFilter))]
        public IActionResult DeleteToDo(int id)
        {
            var result = _toDoRepository.GetToDo(id);

            _toDoRepository.DeleteToDo(id);

            if (!_toDoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}