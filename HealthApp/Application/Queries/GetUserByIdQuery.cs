using System;
using AutoMapper;
using HealthApp.Application.DTOs;
using HealthApp.Core;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace HealthApp.Application.Queries
{
    
    public class GetUserByIdQuery //: BaseGetEntityByIdQuery<UserDto>
    {
        private readonly DatabaseContext _dbContext;

        public GetUserByIdQuery() { 
            
        }

        /*
        public override Task<Core.Entities.User?> GetEntity(BaseGetEntityByIdQuery<UserDto> request)
        {
            return _dbContext.Users.FirstOrDefaultAsync(i => i.Id == request.Id);
        }
        */

        public Task<Core.Entities.User?> GetUserEntity(int Id)
        {
            return _dbContext.Users.FirstOrDefaultAsync(i => i.Id == Id);
        }


    }
    /*
    public class GetPositionByIdQueryHandler : BaseGetEntityByIdQueryHandler<GetUserByIdQuery, UserDto, Core.Entities.User>
    {
        private readonly IUserDbContext _dbContext;
        public GetPositionByIdQueryHandler(IUserDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public override Task<Core.Entities.User?> GetEntity(BaseGetEntityByIdQuery<UserDto> request)
        {
            return _dbContext.Users.FirstOrDefaultAsync(i => i.Id == request.Id);
        }
    }
    */
}

