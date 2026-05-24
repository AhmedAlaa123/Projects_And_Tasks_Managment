using Ui.Services.Dtos;

namespace Ui.Services;

 
    public interface IProjectService
    {
        Task<BaseResponse<PagedResult<ProjectDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<BaseResponse<ProjectDto>?> GetByIdAsync(int id);
        Task CreateAsync(CreateProjectDto dto);
        Task UpdateAsync(int id, UpdateProjectDto dto);
        Task DeleteAsync(int id);
     Task<BaseResponse<List<TaskDto>>> GetProjectTasks(int id);
     
    }

 
