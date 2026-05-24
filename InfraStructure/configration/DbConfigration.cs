using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.configration;

public static class DbConfigration
{
    public static ModelBuilder ConfigureFK(this ModelBuilder modelBuilder) 
    {

        modelBuilder.Entity<Project>().HasMany(ele => ele.AssignedTasks).WithOne(ele => ele.Project)
            .HasForeignKey(ele => ele.ProjectId).HasConstraintName("FK_Project_Task").OnDelete(DeleteBehavior.Restrict);
        ;

        modelBuilder.Entity<ApplicationUser>().HasMany(ele => ele.CreatedProjects).WithOne(ele => ele.CreatorUser)
           .HasForeignKey(ele => ele.CreatedBy).HasConstraintName("FK_Users_Projects").OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<ApplicationUser>().HasMany(ele => ele.CreatedTasks).WithOne(ele => ele.CreatorUser)
          .HasForeignKey(ele => ele.CreatedBy).HasConstraintName("FK_Users_Tasks").OnDelete(DeleteBehavior.SetNull);
        return modelBuilder;
    } 
}
