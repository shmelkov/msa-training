using Microsoft.AspNetCore.Identity;

namespace Users.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser, IAuditableEntity, IEntity<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ICollection<ApplicationUserGroup> UserGroups { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
        
        public Guid? ModifiedById { get; set; }
        
        public Guid? CreatedById { get; set; }
        
        public DateTime Modified { get; set; }
        
        public DateTime Created { get; set; }

        public Guid? EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
