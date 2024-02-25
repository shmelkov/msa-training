namespace Users.Core.Entities
{
    public class ApplicationUserGroup : BaseAuditableEntity<ApplicationUser>
    {
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }
        public ApplicationGroup Group { get; set; }
        public Guid GroupId { get; set; }

    }
}
