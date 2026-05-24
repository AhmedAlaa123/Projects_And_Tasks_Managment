using System.Net;
using Ui.Services.Dtos;

namespace Ui.Services;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient) => _httpClient = httpClient;
 
    
  
    public async Task CreateAsync(CreateTaskDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/Task/Create", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to create task");
        }
    }

  
    public async Task UpdateAsync(int id, UpdateTaskDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/v1/Task/Update", dto);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new Exception($"Task {id} not found");
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to update task");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/Task/Delete/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new Exception($"Task {id} not found");
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to delete task");
        }
    }
    public async Task UpdateStatusAsync(int id, TaskUpdateStatusDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/v1/Task/Update-Status", dto);

        if (response.StatusCode == HttpStatusCode.NotFound)
        { throw new Exception($"Task {id} not found"); }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to update task status");
        }
    }
}
