using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Entities;

namespace ToDoApp.Services
{
    public interface IToDoRepository
    {
        IEnumerable<ToDo> GetToDos();
        ToDo GetToDo(int id);
        void AddToDo(ToDo toDo);
        bool Save();
        void DeleteToDo(int id);
    }
}
