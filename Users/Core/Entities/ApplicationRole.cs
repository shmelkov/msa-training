using Microsoft.AspNetCore.Identity;

namespace Users.Core.Entities
{
    public class ApplicationRole : IdentityRole<Guid>, IAuditableEntity<ApplicationUser>, IEntity<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }

        public ICollection<ApplicationGroupRole> GroupRoles { get; set; }
        public string? DisplayName { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
        //public Guid? ModuleId { get; set; }
        //public ApplicationModule Module { get; set; }
    }
}
