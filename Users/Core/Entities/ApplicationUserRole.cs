using Microsoft.AspNetCore.Identity;

namespace Users.Core.Entities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>, IAuditableEntity<ApplicationUser>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
        public ApplicationUser ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
