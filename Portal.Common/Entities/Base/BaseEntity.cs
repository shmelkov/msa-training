namespace Portal.Common.Entities.Base
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
