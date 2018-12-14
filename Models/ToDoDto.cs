using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDoDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Task { get; set; }

        [Required]
        public bool Completed { get; set; }

        [Required]
        public bool SuitableForChild { get; set; }
    }
}
