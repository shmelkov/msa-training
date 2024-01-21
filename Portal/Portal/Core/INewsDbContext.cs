using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Common;

namespace Portal.Core
{
    public interface INewsDbContext : IDbContext
    {
        public DbSet<News> News { get; set; }
    }
}
