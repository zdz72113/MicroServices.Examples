using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class TodoViewModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
