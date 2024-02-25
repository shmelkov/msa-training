using AutoMapper;
using Users.Application.CQRS.Users.DTOs;
//using Portal.Users.Core.Entities;
//using Portal.Users.Core;
using Users.Common.CQRS.Queries;
using Users.Core.Repositories;
using Users.Core.Entities;

namespace Users.Application.CQRS.Users.Queries
{
    public class GetUsersListQuery : BaseGetEntitiesQuery<UserDto>
    {
    }

    public class GetUsersListQueryHandler : BaseGetEntitiesQueryHandler<GetUsersListQuery, UserDto, ApplicationUser>
    {
        public GetUsersListQueryHandler(IUsersDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
