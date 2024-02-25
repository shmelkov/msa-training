using Users.Application.CQRS.Employees.DTOs;

namespace Users.Application.CQRS.Users.DTOs
{
    public class UserDto : BaseDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public Guid? EmployeeId { get; set; }

        //public string PhotoUrl { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
