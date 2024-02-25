namespace Users.Core.Entities
{
    public abstract class BaseAuditableEntity<T, K> : BaseEntity<T>, IAuditableEntity<K>
        where K : IApplicationUser
    {
        public Guid? ModifiedById { get; set; }

        public K ModifiedBy { get; set; }

        public DateTime Modified { get; set; } = DateTime.UtcNow;

        public Guid? CreatedById { get; set; }

        public K CreatedBy { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }

    public abstract class BaseAuditableEntity<T> : BaseAuditableEntity<Guid, T>
        where T : IApplicationUser
    {

    }

    public abstract class BaseAuditableEntity : BaseAuditableEntity<BaseApplicationUser>
    {
    }

    public interface IAuditableEntity<T> : IAuditableEntity where T : IApplicationUser
    {
        T ModifiedBy { get; set; }

        T CreatedBy { get; set; }
    }

    public interface IAuditableEntity
    {
        Guid? ModifiedById { get; set; }

        Guid? CreatedById { get; set; }

        DateTime Modified { get; set; }

        DateTime Created { get; set; }
    }
}
