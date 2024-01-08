using HealthApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
//using HealthApp.Core.Entities;
//using HealthApp.Services.Interfaces;

namespace HealthApp.Core
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<T> Entry<T>(T entity) where T : class;

        DbSet<T> Set<T>() where T : class;
    }

 

}
