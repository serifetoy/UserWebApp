using Microsoft.EntityFrameworkCore;
using UserWebApp.Models;
using Task = UserWebApp.Models.Task;

namespace UserWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
