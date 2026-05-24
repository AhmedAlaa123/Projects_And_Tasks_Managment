
namespace Application.Contracts;
public interface IProjectCacheService
{
    Task<Project?> GetProjectAsync(int id);
    System.Threading.Tasks.Task SetProjectAsync(Project project, TimeSpan? expiry = null);
    System.Threading.Tasks.Task DeleteProjectAsync(int id);
    System.Threading.Tasks.Task<List<Project>?> GetAllProjectsAsync();
    System.Threading.Tasks.Task SetAllProjectsAsync(List<Project> projects, TimeSpan? expiry = null);
    System.Threading.Tasks.Task DeleteAllProjectsAsync();
}
