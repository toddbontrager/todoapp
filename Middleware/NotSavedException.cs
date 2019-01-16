using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Middleware
{
    public class NotSavedException : Exception
    {
        public int StatusCode { get; set; }

        public NotSavedException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
