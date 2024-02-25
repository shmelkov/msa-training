using Microsoft.AspNetCore.Http;
using Users.Common.Services.Interfaces;
using System.Security.Claims;

namespace Users.Common.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ClaimsPrincipal User => _accessor.HttpContext?.User;

        public Guid UserId => !String.IsNullOrEmpty(User?.FindFirstValue(ClaimTypes.NameIdentifier)) ? Guid.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier)) : Guid.Empty;

        public string Email => User?.FindFirstValue(ClaimTypes.Email);

        public string UserName => User?.FindFirstValue(ClaimTypes.Name);

        public string[] Roles => User?.FindAll(ClaimTypes.Role).Select(i => i.Value).ToArray();

        public string FirstName => User?.FindFirstValue(ClaimTypes.GivenName);

        public string LastName => User.FindFirstValue(ClaimTypes.Surname);

        public Guid? EmployeeId => !String.IsNullOrEmpty(User.FindFirstValue("employeeId")) ? Guid.Parse(User.FindFirstValue("employeeId")) : null;
    }
}
