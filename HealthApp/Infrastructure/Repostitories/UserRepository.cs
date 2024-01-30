using Infrastructure.EntityFramework;
using HealthApp.Core.Entities;
using HealthApp.Core.Repositories;
using HealthApp.Infrastructure.Repostitories.Base;
using System;

namespace HealthApp.Infrastructure.Repostitories
{
    public class UserRepository: BaseRepository<User, DatabaseContext>, IUserRepository
    {
        private readonly DatabaseContext appDbContext;

        public UserRepository(DatabaseContext context) : base(context)
        {
            this.appDbContext = context;
        }
    }
}
