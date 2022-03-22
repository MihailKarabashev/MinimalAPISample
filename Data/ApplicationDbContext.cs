using Microsoft.EntityFrameworkCore;
using MinimalAPISample.Models;

namespace MinimalAPISample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<News> News { get; set; }

        public DbSet<Tag> Tags { get; set; }

    }
}
