using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

// this is a dummy store for local in-memory only
// Just for testing API before database integration
namespace ToDoApp
{
    public class ToDoDataStore
    {
        public static ToDoDataStore Current { get; } = new ToDoDataStore();
        public List<ToDoDto> ToDos { get; set; }

        public ToDoDataStore()
        {
            // init dummy data
            ToDos = new List<ToDoDto>()
            {
                new ToDoDto()
                {
                    Id = 1,
                    Task = "Mow lawn"
                },
                new ToDoDto()
                {
                    Id = 2,
                    Task = "Clean garage"
                }
            };
        }
    }
}
