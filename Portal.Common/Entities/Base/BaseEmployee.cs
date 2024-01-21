namespace Portal.Common.Entities.Base
{
    public interface IEmployee
    {
        public string? FullName { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public Guid? PositionId { get; set; }

        public string? Position { get; set; }

        public Guid? DepartmentId { get; set; }

        public string? Department { get; set; }

        public string? PhotoUrl { get; set; }
    }

    public abstract class BaseEmployee<T> : BaseAuditableEntity<T>, IEmployee
        where T : IApplicationUser
    {
        public string? FullName { get; set; }

        public string? FirstName { get; set; }
        
        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public Guid? PositionId { get; set; }

        public string? Position { get; set; }

        public Guid? DepartmentId { get; set; }

        public string? Department { get; set; }

        public string? PhotoUrl { get; set; }
    }

    public class BaseEmployee : BaseEmployee<BaseEmployeeApplicationUser>
    {
    }
}
