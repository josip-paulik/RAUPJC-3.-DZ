using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApplication.Models
{
    public class AddTodoViewModel
    {
        [Required]
        public string Text { get; set; }
    }
}
