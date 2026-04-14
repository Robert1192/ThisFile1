using Microsoft.EntityFrameworkCore;
using ProjectWebsite10032026.Models;

namespace ProjectWebsite10032026.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Link> Links { get; set; }
        public DbSet<Download> Downloads { get; set; }
    }
}
