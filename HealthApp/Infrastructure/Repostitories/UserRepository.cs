using Infrastructure.EntityFramework;
using HealthApp.Core.Entities;
using HealthApp.Core.Repositories;
using HealthApp.Infrastructure.Repostitories.Base;

namespace HealthApp.Infrastructure.Repostitories
{
    public class UserRepository: BaseRepository<User, DatabaseContext>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
