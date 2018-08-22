using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoViewModel>> GetTodos();

        Task<IEnumerable<string>> GetMachineNames();
    }
}
