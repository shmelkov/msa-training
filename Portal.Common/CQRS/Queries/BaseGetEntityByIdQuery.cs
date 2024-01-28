using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Portal.Common.Entities.Base;

namespace Portal.Common.CQRS.Queries
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

            return entity == null ? throw new Exception() : _mapper.Map<TDto>(entity);
        }

        public virtual Task<TEntity?> GetEntity(BaseGetEntityByIdQuery<TDto> request)
        {
            return _dbContext.Set<TEntity>()
                             .FirstOrDefaultAsync(i => i.Id == request.Id);
        }
    }
}
