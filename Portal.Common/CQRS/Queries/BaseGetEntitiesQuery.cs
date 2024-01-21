using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Portal.Common.DTOs;
using Portal.Common.Extensions;
using Portal.Common.Requests;

namespace Portal.Common.CQRS.Queries
{
    public interface IGetEntitiesQuery
    {
    }

    public abstract class BaseGetEntitiesQuery<T> : BasePagingRequest<IPagedDto<T>>, IGetEntitiesQuery
    {
    }

    public abstract class BaseGetEntitiesQueryHandler<TQuery, TDto, TEntity> : IRequestHandler<TQuery, IPagedDto<TDto>>
        where TQuery : BaseGetEntitiesQuery<TDto>
        where TEntity : class
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        protected BaseGetEntitiesQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<IPagedDto<TDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var query = GetQueryable(request);

            var entities = await query
                                .ApplyPagingOptions(request.PagingOptions)
                                .ToListAsync();

            return new BasePagedDto<TDto>
            {
                TotalCount = await query.CountAsync(),
                Offset = request.PagingOptions.Offset,
                Data = _mapper.Map<IEnumerable<TDto>>(entities),
                Count = entities.Count
            };
        }

        public virtual IQueryable<TEntity> GetQueryable(TQuery request) 
        {
            return _dbContext.Set<TEntity>()
                                   .AsNoTracking()
                                   .ApplyFilterOptions(request.FilterOptions)
                                   .ApplySortOptions(request.SortOptions);
        }
    }
}
