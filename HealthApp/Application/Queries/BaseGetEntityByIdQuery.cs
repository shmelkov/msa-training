﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using HealthApp.Core.Entities;
using HealthApp.Core;
//using Portal.Common.Exceptions;

namespace HealthApp.Application.Queries
{
    public interface IGetEntityByIdQuery
    {
        public int Id { get; set; }
    }

    public abstract class BaseGetEntityByIdQuery<TDto> : IRequest<TDto>, IGetEntityByIdQuery
    {
        public int Id { get; set; }
    }

    public abstract class BaseGetEntityByIdQueryHandler<TQuery, TDto, TEntity> : IRequestHandler<TQuery, TDto>
        where TQuery : BaseGetEntityByIdQuery<TDto>
        where TEntity : class, IEntity<int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        protected BaseGetEntityByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<TDto> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var entity = await GetEntity(request);

            if (entity == null)
            {
                //throw new NotFoundException();
            }

            return _mapper.Map<TDto>(entity);
        }

        public virtual Task<TEntity?> GetEntity(BaseGetEntityByIdQuery<TDto> request)
        {
            return _dbContext.Set<TEntity>()
                             .FirstOrDefaultAsync(i => i.Id == request.Id);
        }
    }
}