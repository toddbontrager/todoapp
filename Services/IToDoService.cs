using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Entities;
using ToDoApp.Models;
using Microsoft.AspNetCore.Http;

namespace ToDoApp.Services
{
    public interface IToDoService
    {
        IEnumerable<ToDoDto> GetAllToDos(HttpContext context);
        ToDo GetToDoById(int id);
        ToDoDto CreateToDo(ToDoDto toDo);
        void UpdateToDo(int id, ToDoDto toDo);
        void DeleteToDo(int id);
    }
}
