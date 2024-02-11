using Refit;

namespace AppGateway.Helpers
{
    public interface IUserServiceApi
    {
        [Get("/users/{id}")]
        Task<UserInfo> GetUserInfo(string id);

        [Post("/users")]
        Task CreateUser([Body] UserInfo userInfo, [Authorize("Bearer")] string token);

    }

    public class UserInfo
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string EmployeeId { get; set; }

        public IEnumerable<string> Roles { get; set; } = new string[] {};
    }
}
