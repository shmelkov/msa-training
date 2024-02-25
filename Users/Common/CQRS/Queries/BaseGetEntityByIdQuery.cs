using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
//using Portal.Common.Entities.Base;
//using Portal.Common.Exceptions;
using Users.Core.Repositories;
using Users.Core.Entities;

namespace Users.Common.CQRS.Queries
{
    public interface IGetEntityByIdQuery
    {
        public Guid Id { get; set; }
    }

    public abstract class BaseGetEntityByIdQuery<TDto> : IRequest<TDto>, IGetEntityByIdQuery
    {
        public Guid Id { get; set; }
    }

    public abstract class BaseGetEntityByIdQueryHandler<TQuery, TDto, TEntity> : IRequestHandler<TQuery, TDto>
        where TQuery : BaseGetEntityByIdQuery<TDto>
        where TEntity : class, IEntity<Guid>
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

            return entity == null ? throw new Exception("Not found") : _mapper.Map<TDto>(entity);
        }

        public virtual Task<TEntity?> GetEntity(BaseGetEntityByIdQuery<TDto> request)
        {
            return _dbContext.Set<TEntity>()
                             .FirstOrDefaultAsync(i => i.Id == request.Id);
        }
    }
}
