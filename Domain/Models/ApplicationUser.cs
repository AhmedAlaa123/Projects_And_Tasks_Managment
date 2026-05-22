using Microsoft.AspNetCore.Identity;
namespace Domain.Models;
public class ApplicationUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public virtual ICollection<Task> CreatedTasks { get; set; }
    public virtual ICollection<Project> CreatedProjects { get; set; }
}

