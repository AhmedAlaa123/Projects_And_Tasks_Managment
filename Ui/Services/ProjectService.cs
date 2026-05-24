using System.Net;
using Ui.Services.Dtos;

namespace Ui.Services;

public class ProjectService : IProjectService
{
    private readonly HttpClient _httpClient;

    public ProjectService(HttpClient httpClient) => _httpClient = httpClient;

 
    public async Task<BaseResponse<PagedResult<ProjectDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        var response = await _httpClient
            .GetAsync($"api/v1/Projects/GetAll?PageNumber={pageNumber}&PageSize={pageSize}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to get projects");
        }

        return await response.Content.ReadFromJsonAsync<BaseResponse<PagedResult<ProjectDto>>>() ?? new();
    }

 
    public async Task<BaseResponse<ProjectDto>?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/v1/Projects/Get/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        { throw new Exception($"Project {id} not found"); }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to get project");
        }

        return await response.Content.ReadFromJsonAsync<BaseResponse<ProjectDto>>();
    }

 
    public async Task CreateAsync(CreateProjectDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/Projects/Create", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to create project");
        }
    }

 
    public async Task UpdateAsync(int id, UpdateProjectDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/v1/Projects/Update", dto);

        if (response.StatusCode == HttpStatusCode.NotFound)
        { throw new Exception($"Project {id} not found"); }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to update project");
        }
    }

 
    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/v1/Projects/Delete/{id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        { throw new Exception($"Project {id} not found"); }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to delete project");
        }
    }
    public async Task<BaseResponse<List<TaskDto>>> GetProjectTasks(int id)
    {
        var response = await _httpClient.GetAsync($"api/v1/Projects/get-tasks?id={id}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        { throw new Exception($"Project {id} not found"); }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? "Failed to get project tasks");
        }

        return await response.Content.ReadFromJsonAsync<BaseResponse<List<TaskDto>>>();
    }
}
