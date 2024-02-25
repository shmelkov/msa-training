using AutoMapper;
using Users.Application.CQRS.Users.DTOs;
//using Portal.Users.Core;
using Users.Core.Entities;
using Users.Common.CQRS.Queries;
using Users.Core.Repositories;

namespace Users.Application.CQRS.Users.Queries
{
    public class GetUsersQuery : BaseGetEntitiesQuery<UserDto>
    {
    }

    public class GetUsersQueryHandler : BaseGetEntitiesQueryHandler<GetUsersQuery, UserDto, ApplicationUser>
    {   
        public GetUsersQueryHandler(IMapper mapper, IUsersDbContext dbContext) : base(dbContext, mapper)
        {   
        }
    }
}
