namespace Users.Core.Entities
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
    }
}
