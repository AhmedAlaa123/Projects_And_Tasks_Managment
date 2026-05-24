using Ui.Services.Dtos;

namespace Ui.Services;

public interface ITaskService
{
 
    Task CreateAsync(CreateTaskDto dto);
    Task UpdateAsync(int id, UpdateTaskDto dto);
    Task DeleteAsync(int id);
    Task UpdateStatusAsync(int id, TaskUpdateStatusDto dto);
}
