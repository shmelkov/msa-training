using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Portal.Common.Entities.Base;

namespace Portal.Common
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<T> Entry<T>(T entity) where T : class;

        DbSet<T> Set<T>() where T : class;
    }

    
}
