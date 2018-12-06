using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Services
{
    public class ToDoRepository : IToDoRepository
    {
        private ToDoContext _context;
        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }        

        public IEnumerable<ToDo> GetToDos()
        {
            return _context.ToDos.OrderBy(t => t.Task).ToList();
        }

        public ToDo GetToDo(int id)
        {
            return _context.ToDos.Where(t => t.Id == id).FirstOrDefault();
        }

        public void AddToDo(ToDo toDo)
        {
            _context.ToDos.Add(toDo);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteToDo(int id)
        {
            _context.Remove(GetToDo(id));
        }
    }
}
