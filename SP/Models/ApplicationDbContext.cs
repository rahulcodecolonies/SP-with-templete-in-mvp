using Microsoft.EntityFrameworkCore;

namespace SP.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<UserLogin> Account { get; set; }
    }
}
