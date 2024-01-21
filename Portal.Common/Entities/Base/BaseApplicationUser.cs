namespace Portal.Common.Entities.Base
{
    public class BaseApplicationUser<T> : BaseEntity<T>, IAuditableEntity, IApplicationUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid? ModifiedById { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }

    public class BaseApplicationUser : BaseApplicationUser<Guid>
    {

    }

    public class BaseEmployeeApplicationUser<T> : BaseApplicationUser
        where T : IEmployee
    {
        public Guid? EmployeeId { get; set; }

        public T? Employee { get; set; }
    }

    public class BaseEmployeeApplicationUser : BaseEmployeeApplicationUser<BaseEmployee>
    {   
    }

    public interface IApplicationUser
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
    }
}
