
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using TaskManagement.SharedData;
using static System.Net.WebRequestMethods;


namespace TaskManagement.Client
{ 
    public class TaskApi
    {
        private readonly HttpClient _httpClient;
        public TaskApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Tasks>?> GetTasks() 
        {
           var returnTasks= _httpClient.GetFromJsonAsync<List<Tasks>>("api/Tasks"); 
            return returnTasks;
        }

        public Task Update(Tasks T)
        {
            var updatedTask = _httpClient.PutAsJsonAsync($"api/Tasks/{T.Id}", T);
            return updatedTask;
        }
        public async Task<(bool ok, Tasks? task, string? error)> Create(Tasks t)
        {
            var res = await _httpClient.PostAsJsonAsync("api/Tasks", t);

            if (res.StatusCode == System.Net.HttpStatusCode.Conflict)
                return (false, null, "Task already exists.");

            if (!res.IsSuccessStatusCode)
            {
                var msg = await res.Content.ReadAsStringAsync();
                return (false, null, $"Error {res.StatusCode}: {msg}");
            }

            var created = await res.Content.ReadFromJsonAsync<Tasks>();
            return (true, created, null);
        }

        public Task Delete(Guid id)
        {
            var deletedTask = _httpClient.DeleteAsync($"api/Tasks/{id}");
            return deletedTask;
        }
    }
}
