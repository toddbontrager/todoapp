using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoMapper;
using ToDoApp.Models;
using ToDoApp.Entities;
using ToDoApp.Repository;
using ToDoApp.Middleware;
using Microsoft.AspNetCore.Http;

namespace ToDoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public IEnumerable<ToDoDto> GetAllToDos(HttpContext context)
        {
            var currentUser = context.User;
            var userAge = FindUserAge(currentUser);
            var toDoEntities = _toDoRepository.GetToDos();
            var results = Mapper.Map<IEnumerable<ToDoDto>>(toDoEntities);
            
            if (userAge < 14)
            {
                results = results.Where(t => t.SuitableForChild).ToArray();
            }
            return results;
        }

        public ToDo GetToDoById(int id)
        {
            return _toDoRepository.GetToDo(id);
        }

        public ToDoDto CreateToDo(ToDoDto toDo)
        {
            var mapped = Mapper.Map<Entities.ToDo>(toDo);
            _toDoRepository.AddToDo(mapped);

            if (!_toDoRepository.Save())
            {
                throw new NotSavedException(500, "A problem happened while handling your request.");
            }

            var createdToDoToReturn = Mapper.Map<ToDoDto>(mapped);
            return createdToDoToReturn;
        }

        public void UpdateToDo(int id, ToDoDto toDo)
        {
            var result = _toDoRepository.GetToDo(id);
            Mapper.Map(toDo, result);

            if (!_toDoRepository.Save())
            {
                throw new NotSavedException(500, "A problem happened while handling your request.");
            }
        }

        public void DeleteToDo(int id)
        {
            _toDoRepository.DeleteToDo(id);
            if (!_toDoRepository.Save())
            {
                throw new NotSavedException(500, "A problem happened while handling your request.");
            }
        }

        private int FindUserAge(ClaimsPrincipal currentUser)
        {
            var userAge = 0;
            if (UserHasClaim(currentUser))
            {
                DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
                userAge = DateTime.Today.Year - birthDate.Year;
            }
            return userAge;
        }

        private bool UserHasClaim(ClaimsPrincipal currentUser)
        {
            if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return true;
            }
            return false;
        }
    }
}
