using System;
using apiv2.Models;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Data
{
    public class ProjectContext: DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options): base(options)
        {
        }

        public DbSet<Apprentice> Apprentices { get; set; }
        public DbSet<Department> Departments { get; set; }

     
    }
}
