using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalHub.Models;

namespace PersonalHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonalHub.Models.Schedule> Schedules { get; set; } 

        public DbSet<PersonalHub.Models.Category> Category { get; set; }
    }
}