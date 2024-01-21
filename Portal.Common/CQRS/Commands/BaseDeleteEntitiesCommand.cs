using MediatR;
using Microsoft.EntityFrameworkCore;
using Portal.Common.Entities.Base;
using Portal.Common.Extensions;
using Portal.Common.Helpers;
using Portal.Common.Requests;

namespace Portal.Common.CQRS.Commands
{
    public interface IDeleteEntitiesCommand : IFilteringRequest
    {
        public string? Filter { get; set; }
        public FilterOptions FilterOptions { get; set; }
    }

    public abstract class BaseDeleteEntitiesCommand : IRequest, IDeleteEntitiesCommand
    {
        public string? Filter { get; set; }
        public FilterOptions FilterOptions { get; set; }
    }

    public abstract class BaseDeleteEntitiesCommandHandler<TCommand, TEntity> : IRequestHandler<TCommand>
        where TCommand : BaseDeleteEntitiesCommand
        where TEntity : class, IEntity<Guid>
    {
        private readonly IDbContext _dbContext;

        protected BaseDeleteEntitiesCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            CheckFilter(request);

            var query = _dbContext.Set<TEntity>()
                                   .AsNoTracking()
                                   .ApplyFilterOptions(request.FilterOptions);

            _dbContext.Set<TEntity>().RemoveRange(query);

            await _dbContext.SaveChangesAsync();
        }

        public virtual void CheckFilter(TCommand request)
        {
            var ids = request.FilterOptions.FilterFields[0].Value;

            if (ids is object[] array && array.OfType<Guid>().Any(i => i == Guid.Empty) || (Guid)ids == Guid.Empty)
            {
                throw new ApplicationException("Filter contains empty value!");
            }
        }
    }
}
