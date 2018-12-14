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
        [HttpGet, Authorize]
        public IActionResult GetTodos()
        {
            var currentUser = HttpContext.User;
            int userAge = 0;

            var toDoEntities = _toDoRepository.GetToDos();
            var results = Mapper.Map<IEnumerable<ToDoDto>>(toDoEntities);

            // TODO: Refactor into a service
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
                userAge = DateTime.Today.Year - birthDate.Year;
            }          

            if (userAge < 14)
            {
                results = results.Where(t => t.SuitableForChild).ToArray();
            }

            return Ok(results);
        }

        // GET api/todos/2
        [HttpGet("{id}", Name ="GetToDo")]
        public IActionResult GetToDo(int id)
        {
            var result = _toDoRepository.GetToDo(id);

            if (result == null)
             {
                 return NotFound();
             }

             return Ok(result);
        }

        // POST api/todos
        [HttpPost]
        public IActionResult CreateToDo([FromBody] ToDoDto toDo)
        {
            if (toDo == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            var result = _toDoRepository.GetToDo(id);
            if (result == null)
            {
                return NotFound();
            }

            Mapper.Map(toDo, result);

            if (!_toDoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            var result = _toDoRepository.GetToDo(id);
            if (result == null)
            {
                return NotFound();
            }

            _toDoRepository.DeleteToDo(id);

            if (!_toDoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}