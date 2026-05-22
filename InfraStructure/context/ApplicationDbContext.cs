using Domain.Models;
using InfraStructure.configration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace InfraStructure.context;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
            
    }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Domain.Models.Task> Tasks{ get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ConfigureFK();
        base.OnModelCreating(builder);
    }
}
