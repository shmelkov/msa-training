using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HealthApp.Core.Entities;
using HealthApp.Infrastructure;

namespace Infrastructure.EntityFramework
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(x => x.Id);

            DataSeed.SeedUsers(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);   
        }
    }
}