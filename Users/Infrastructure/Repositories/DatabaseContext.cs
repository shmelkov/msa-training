using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using Portal.Common;
using Users.Common.Services.Interfaces;
//using Portal.Users.Core;
using Users.Core.Entities;
using Users.Core.Repositories;

namespace Users.Infrastructure.Repositories
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IUsersDbContext
    {
        private readonly IUserAccessor _userAccessor;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IUserAccessor userAccessor) : base(options)
        {
            _userAccessor = userAccessor;
        }

        //public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        //{
        //    //_userAccessor = userAccessor;
        //}

        //public DbSet<UsersModuleSetting> UsersModuleSettings { get; set; }

        //public DbSet<ProfileSetting> ProfileSettings { get; set; }

        //public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<LinkCategory> LinkCategories { get; set; }
        //public DbSet<EmployeeLink> EmployeeLinks { get; set; }
        public DbSet<ApplicationGroup> Groups { get; set; }
        //public DbSet<ApplicationModule> Modules { get; set; }
        //public DbSet<EmployeeHobby> EmployeeHobbies { get; set;}

        //public DbSet<Skill> Skills { get; set; }

        //public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        //public DbSet<Department> Departments { get; set; }

        //public DbSet<Position> Positions { get; set; }
        //public DbSet<EmployeeSettings> EmployeeSettings { get; set; }

        public DbSet<ApplicationUserGroup> GroupUsers { get; set; }

        public DbSet<ApplicationGroupRole> GroupRoles { get; set; }
        //public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().Property(i => i.FullName)
                .HasColumnType("citext");
            modelBuilder.Entity<Employee>().Property(i => i.FirstName)
                .HasColumnType("citext");
            modelBuilder.Entity<Employee>().Property(i => i.MiddleName)
                .HasColumnType("citext");
            modelBuilder.Entity<Employee>().Property(i => i.LastName)
                .HasColumnType("citext");
            //modelBuilder.Entity<Employee>().Property(i => i.PhotoUrl)
            //    .HasColumnType("citext");
            //modelBuilder.Entity<Employee>().Property(i => i.PersonnelNumber)
                //.HasColumnType("citext");
            modelBuilder.Entity<Employee>().Property(i => i.UserName)
                .HasColumnType("citext");
            modelBuilder.Entity<Employee>().Property(i => i.Email)
                .HasColumnType("citext");
            //modelBuilder.Entity<Employee>().Property(i => i.WorkPhone)
            //    .HasColumnType("citext");
            //modelBuilder.Entity<Employee>().Property(i => i.Location)
            //    .HasColumnType("citext");

            //modelBuilder.Entity<Position>().Property(i => i.Title)
            //    .HasColumnType("citext");

            //modelBuilder.Entity<Company>().Property(i => i.Title)
            //    .HasColumnType("citext");

            modelBuilder.Entity<ApplicationUserRole>().HasKey(sc => new { sc.UserId, sc.RoleId });
            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<LinkCategory>().HasKey(lk => lk.Id);

            //modelBuilder.Entity<EmployeeLink>().HasKey(el => el.Id);

            modelBuilder.Entity<ApplicationUserGroup>().HasKey(sc => new { sc.UserId, sc.GroupId });
            modelBuilder.Entity<ApplicationUserGroup>().HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUserGroup>().HasOne(u => u.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ApplicationUserGroup>().HasOne(u => u.CreatedBy)
                .WithMany()
                .HasForeignKey(u => u.CreatedById);
            modelBuilder.Entity<ApplicationUserGroup>().HasOne(u => u.ModifiedBy)
                .WithMany()
                .HasForeignKey(u => u.ModifiedById);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(u => u.CreatedBy)
                .WithMany()
                .HasForeignKey(u => u.CreatedById);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(u => u.ModifiedBy)
                .WithMany()
                .HasForeignKey(u => u.ModifiedById);
            //modelBuilder.Entity<Skill>().HasKey(s => s.Id);
            //modelBuilder.Entity<EmployeeSkill>().HasKey(es => es.Id);


            modelBuilder.Entity<ApplicationGroupRole>().HasKey(sc => new { sc.RoleId, sc.GroupId });

            modelBuilder.Entity<ApplicationGroupRole>().HasOne(ug => ug.Group)
                .WithMany(g => g.GroupRoles)
                .HasForeignKey(ug => ug.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationGroupRole>().HasOne(u => u.Role)
                .WithMany(u => u.GroupRoles)
                .HasForeignKey(ug => ug.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationGroupRole>().HasOne(u => u.CreatedBy)
                .WithMany()
                .HasForeignKey(u => u.CreatedById);

            modelBuilder.Entity<ApplicationGroupRole>().HasOne(u => u.ModifiedBy)
                .WithMany()
                .HasForeignKey(u => u.ModifiedById);

            //modelBuilder.Entity<ApplicationRole>().HasOne(u => u.Module)
            //    .WithMany()
            //    .HasForeignKey(u => u.ModuleId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Position>().HasKey(sc => sc.Id);

            //modelBuilder.Entity<Department>().HasOne(d => d.Manager)
            //    .WithMany()
            //    .HasForeignKey(d => d.ManagerId);

            //modelBuilder.Entity<Department>().HasOne(p => p.Manager)
            //    .WithMany()
            //    .HasForeignKey(p => p.ManagerId);

            //modelBuilder.Entity<EmployeeSettings>().HasKey(sc => sc.Id);

            //modelBuilder.Entity<Position>().HasOne(p => p.Employee)
            //    .WithMany(e => e.Positions)
            //    .HasForeignKey(p => p.EmployeeId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>().HasOne(u => u.ModifiedBy)
                .WithMany()
                .HasForeignKey(u => u.ModifiedById);

            modelBuilder.Entity<Employee>().HasOne(u => u.CreatedBy)
                .WithMany()
                .HasForeignKey(u => u.CreatedById);

            modelBuilder.Entity<Employee>().HasOne(p => p.User)
                .WithOne(e => e.Employee)
                .HasForeignKey<ApplicationUser>(p => p.EmployeeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FullName)
                 .HasComputedColumnSql(@"CASE WHEN ""MiddleName"" IS NULL THEN ""FirstName"" || ' ' || ""LastName""
                                             ELSE ""FirstName"" || ' ' || ""MiddleName"" || ' ' || ""LastName"" END", stored: true);

            //Seed data
            DataSeed.SeedDefaultUsersAndGroups(modelBuilder);

            //DataSeed.SeedRolesAndModules(modelBuilder);
            //DataSeed.SeedUsersModuleSetting(modelBuilder);
            //DataSeed.SeedEmployeeLinks(modelBuilder);
            //DataSeed.SeedProfileSettings(modelBuilder);
            //DataSeed.SeedEmployeeSkillsAndHobbies(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            BaseDbContext<ApplicationUser>.SaveChanges(this, _userAccessor);
            //BaseDbContext<ApplicationUser>.SaveChanges(this, null);

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}