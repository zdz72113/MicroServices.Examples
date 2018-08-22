using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _apiClient;
        private readonly IOptions<ApiConfig> _setting;

        public TodoService(HttpClient httpClient, IOptions<ApiConfig> settings)
        {
            _apiClient = httpClient;
            _setting = settings;
        }

        public async Task<IEnumerable<TodoViewModel>> GetTodos()
        {
            var url = $"{_setting.Value.TodoApiUrl}/api/todo";
            var dataString = await _apiClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<TodoViewModel>>(dataString);
        }

        public async Task<IEnumerable<string>> GetMachineNames()
        {
            var url = $"{_setting.Value.TodoApiUrl}/api/machine";
            var dataString = await _apiClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<string>>(dataString);
        }
    }
}
