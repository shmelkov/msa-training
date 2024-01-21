using Microsoft.EntityFrameworkCore;
using Portal.Core;
using Portal.Core.Entities;

namespace Portal.Inftrastructure
{
    public class NewsContext : DbContext, INewsDbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options)
        {
        }

        public DbSet<News> News { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DataSeed.SeedNews(modelBuilder);
        }
    }
}
