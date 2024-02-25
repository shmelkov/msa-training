namespace Users.Core.Entities
{
    public class ApplicationGroupRole : BaseAuditableEntity<ApplicationUser>
    {
        public ApplicationRole Role { get; set; }
        public Guid RoleId { get; set; }
        public ApplicationGroup Group { get; set; }
        public Guid GroupId { get; set; }

    }
}
