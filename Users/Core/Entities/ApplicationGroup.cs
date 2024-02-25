namespace Users.Core.Entities
{
    public class ApplicationGroup : BaseAuditableEntity<ApplicationUser>
    {
        public ICollection<ApplicationUserGroup> UserGroups { get; set; }

        public ICollection<ApplicationGroupRole> GroupRoles { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
