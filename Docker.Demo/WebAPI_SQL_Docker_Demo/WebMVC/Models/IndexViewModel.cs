using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class IndexViewModel
    {
        public IEnumerable<TodoViewModel> Todos { get; set; }
        public IEnumerable<string> Machines { get; set; }
    }
}
