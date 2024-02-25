using Microsoft.EntityFrameworkCore;
//using Portal.Common;
using Users.Core.Entities;

namespace Users.Core.Repositories
{
    public interface IUsersDbContext : IDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<ApplicationRole> Roles { get; set; }

        //public DbSet<Hobby> Hobbies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        //public DbSet<LinkCategory> LinkCategories { get; set; }

        //public DbSet<EmployeeLink> EmployeeLinks { get; set; }

        //public DbSet<EmployeeHobby> EmployeeHobbies { get; set; }

        public DbSet<ApplicationGroup> Groups { get; set; }

        //public DbSet<ApplicationModule> Modules { get; set; }

        public DbSet<ApplicationUserGroup> GroupUsers { get; set; }

        public DbSet<ApplicationGroupRole> GroupRoles { get; set; }

        //public DbSet<Skill> Skills { get; set; }

        //public DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        //public DbSet<Department> Departments { get; set; }

        //public DbSet<Company> Companies { get; set; }

        //public DbSet<Position> Positions { get; set; }
        //public DbSet<EmployeeSettings> EmployeeSettings { get; set; }
    }
}
