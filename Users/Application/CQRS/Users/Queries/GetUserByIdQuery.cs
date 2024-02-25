using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Users.Application.CQRS.Users.Commands;
using Users.Application.CQRS.Users.DTOs;
//using Portal.Users.Core;
using Users.Core.Entities;
using Users.Common.CQRS.Queries;
using Users.Core.Repositories;


namespace Users.Application.CQRS.Users.Queries
{
    public class GetUserByIdQuery : BaseGetEntityByIdQuery<UserDto>
    {
    }

    public class GetUserByIdQueryHandler : BaseGetEntityByIdQueryHandler<GetUserByIdQuery, UserDto, ApplicationUser>
    {
        private readonly IUsersDbContext _dbContext;

        public GetUserByIdQueryHandler(IUsersDbContext dbContext, IMapper mapper) : base (dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public override Task<ApplicationUser?> GetEntity(BaseGetEntityByIdQuery<UserDto> request)
        {
            return _dbContext.Users
                                      .AsNoTracking()
                                      .Include(i => i.Employee)
                                      .Include(i => i.UserGroups)
                                      .ThenInclude(i => i.Group)
                                      .ThenInclude(i => i.GroupRoles)
                                      .ThenInclude(i => i.Role)
                                      .FirstOrDefaultAsync(i => i.Id == request.Id);
        }
    }

    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(i => i.Id).NotEmpty();
        }
    }
}
