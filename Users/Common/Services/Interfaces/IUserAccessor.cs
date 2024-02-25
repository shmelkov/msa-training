using System.Security.Claims;

namespace Users.Common.Services.Interfaces
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }

        public Guid UserId { get; }

        public string Email { get; }

        public string UserName { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Guid? EmployeeId { get; }

        public string[] Roles { get; }
    }
}
