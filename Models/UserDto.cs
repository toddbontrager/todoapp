using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
