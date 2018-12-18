using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoApp.Entities;
using ToDoApp.Repository;

namespace ToDoApp.Services
{
    public class ValidateEntityExistsFilter : IActionFilter
    {
        private readonly IToDoRepository _toDoRepository;

        public ValidateEntityExistsFilter(ToDoContext context, IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = (int)context.ActionArguments["id"];

            var result = _toDoRepository.GetToDo(id);
            if (result == null)
            {
                context.Result = new NotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
