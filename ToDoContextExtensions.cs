using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Entities;

namespace ToDoApp
{
    public static class ToDoContextExtensions
    {
        public static void EnsureSeedDataForContext(this ToDoContext context)
        {
            if (context.ToDos.Any())
            {
                return;
            }

            // init seed data
            var todos = new List<ToDo>()
            {
                new ToDo()
                {
                    Task = "Wash car",
                },
                new ToDo()
                {
                    Task = "Rake leaves"
                },
                new ToDo()
                {
                    Task = "Paint house"
                }
            };

            context.ToDos.AddRange(todos);
            context.SaveChanges();
        }
    }
}
