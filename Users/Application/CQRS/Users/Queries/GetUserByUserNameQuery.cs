using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
//using Portal.Common.Exceptions;
using Users.Application.CQRS.Users.DTOs;
//using Portal.Users.Core;
using Users.Core.Entities;
using Users.Core.Repositories;

namespace Users.Application.CQRS.Users.Queries
{
    public class GetUserByUserNameQuery : IRequest<UserDto>
    {
        public string UserName { get; set; }
    }

    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, UserDto>
    {
        private readonly IUsersDbContext _usersDbContext;
        private readonly IMapper _mapper;

        public GetUserByUserNameQueryHandler(IUsersDbContext usersDbContext, IMapper mapper)
        {
            _usersDbContext = usersDbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersDbContext.Users
                                      .AsNoTracking()
                                      .Include(i => i.Employee)
                                      .Include(i => i.UserGroups)
                                      .ThenInclude(i => i.Group)
                                      .ThenInclude(i => i.GroupRoles)
                                      .ThenInclude(i => i.Role)
                                      .SingleOrDefaultAsync(i => i.UserName == request.UserName) ?? throw new Exception("Not found");

            return _mapper.Map<UserDto>(user);
        }
    }
}
