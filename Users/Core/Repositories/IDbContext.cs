using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Users.Core.Entities;
using Users.Common.Services.Interfaces;

namespace Users.Core.Repositories
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<T> Entry<T>(T entity) where T : class;

        DbSet<T> Set<T>() where T : class;
    }

    public interface IEmployeeDbContext<TEmployee, TUser> : IDbContext 
        where TEmployee : class, IEmployee
        where TUser : class, IApplicationUser
    {
        public DbSet<TUser> ApplicationUsers { get; set; }
        public DbSet<TEmployee> Employees { get; set; }
    }

    public interface IEmployeeDbContext<TUser> : IEmployeeDbContext<BaseEmployee, TUser>
        where TUser : class, IApplicationUser
    {
    }

    public interface IEmployeeDbContext : IEmployeeDbContext<BaseEmployee, BaseEmployeeApplicationUser>
    {   
    }

    public abstract class BaseDbContext<T> : DbContext, IDbContext where T : class, IApplicationUser, IEntity<Guid>, new()
    {
        private readonly IUserAccessor _userAccessor;

        public DbSet<T> ApplicationUsers { get; set; }

        protected BaseDbContext(DbContextOptions options, IUserAccessor userAccessor) : base(options)
        {
            _userAccessor = userAccessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddUserIfNotExists(this, _userAccessor);

            SaveChanges(this, _userAccessor);

            return base.SaveChangesAsync(cancellationToken);
        }

        public static void SaveChanges(DbContext dbContext, IUserAccessor userAccessor)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditableEntity
                    && (e.State == EntityState.Added || e.State == EntityState.Modified));

            if (entries.Any())
            {
                var currentTime = DateTime.UtcNow;

                Guid? currentUserId = userAccessor != null && userAccessor.UserId != Guid.Empty
                    ? userAccessor.UserId : null;

                foreach (var entityEntry in entries)
                {
                    var entity = (IAuditableEntity)entityEntry.Entity;

                    entity.Modified = currentTime;
                    entity.ModifiedById = currentUserId;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entity.Created = currentTime;
                        entity.CreatedById = currentUserId;
                    }
                }
            }
        }

        public static void AddUserIfNotExists(BaseDbContext<T> dbContext, IUserAccessor userAccessor)
        {
            Guid? currentUserId = userAccessor != null && userAccessor.UserId != Guid.Empty
                    ? userAccessor.UserId : null;

            if (currentUserId == null)
                return;

            var user = dbContext.ApplicationUsers.Find(currentUserId);

            if (user == null)
            {
                dbContext.ApplicationUsers.Add(
                    new T
                    {
                        Id = currentUserId.Value,
                        UserName = userAccessor.UserName,
                        FirstName = userAccessor.FirstName,
                        LastName = userAccessor.LastName,
                        MiddleName = ""
                    });
            }
        }
    }

    public abstract class BaseDbContext : BaseDbContext<BaseApplicationUser>
    {
        protected BaseDbContext(DbContextOptions options, IUserAccessor userAccessor) : base(options, userAccessor)
        {
        }
    }

    public abstract class BaseEmployeeDbContext<TEmployee,TUser> : BaseDbContext<TUser>, IEmployeeDbContext<TEmployee, TUser>
         where TEmployee : class, IEmployee
         where TUser : class, IApplicationUser, IEntity<Guid>, new()
    {
        protected BaseEmployeeDbContext(DbContextOptions options, IUserAccessor userAccessor) : base(options, userAccessor)
        {
        }

        public DbSet<TEmployee> Employees { get; set; }
    }

    public abstract class BaseEmployeeDbContext<TUser> : BaseEmployeeDbContext<BaseEmployee, TUser>
         where TUser : class, IApplicationUser, IEntity<Guid>, new()
    {
        protected BaseEmployeeDbContext(DbContextOptions options, IUserAccessor userAccessor) : base(options, userAccessor)
        {
        }
    }

    public abstract class BaseEmployeeDbContext : BaseEmployeeDbContext<BaseEmployee, BaseEmployeeApplicationUser>
    {
        protected BaseEmployeeDbContext(DbContextOptions options, IUserAccessor userAccessor) : base(options, userAccessor)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BaseEmployeeApplicationUser>().HasOne(u => u.Employee)
               .WithMany()
               .HasForeignKey(u => u.EmployeeId);
        }
    }
}
