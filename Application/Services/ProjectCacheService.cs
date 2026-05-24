

using System.Text.Json;
using StackExchange.Redis;

namespace Application.Services;
public class ProjectCacheService : IProjectCacheService
{
    private readonly IDatabase _db;
    private const string ProjectKeyPrefix = "project:";
    private const string ProjectListKey = "project:list";
    private static readonly TimeSpan DefaultExpiry = TimeSpan.FromMinutes(30);

    public ProjectCacheService(IConnectionMultiplexer redis)=> _db = redis.GetDatabase();


    public async System.Threading.Tasks.Task<Project?> GetProjectAsync(int id)
    {
        var value = await _db.StringGetAsync($"{ProjectKeyPrefix}{id}");
        return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Project>(value!);
    }

    public async System.Threading.Tasks.Task SetProjectAsync(Project project, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(project);
        await _db.StringSetAsync($"{ProjectKeyPrefix}{project.Id}", json, expiry ?? DefaultExpiry);
    }

    public async System.Threading.Tasks.Task DeleteProjectAsync(int id)
    {
        await _db.KeyDeleteAsync($"{ProjectKeyPrefix}{id}");
    }

    public async System.Threading.Tasks.Task<List<Project>?> GetAllProjectsAsync()
    {
        var value = await _db.StringGetAsync(ProjectListKey);
        return value.IsNullOrEmpty ? null : JsonSerializer.Deserialize<List<Project>>(value!);
    }

    public async System.Threading.Tasks.Task SetAllProjectsAsync(List<Project> projects, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(projects);
        await _db.StringSetAsync(ProjectListKey, json, expiry ?? DefaultExpiry);
    }

    public async System.Threading.Tasks.Task DeleteAllProjectsAsync()
    {
        await _db.KeyDeleteAsync(ProjectListKey);
    }
}
