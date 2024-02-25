using MediatR;
using Microsoft.EntityFrameworkCore;
//using Users.Core.Entities.Base;
using Users.Core.Entities;
//using Portal.Common.Exceptions;
//using Portal.Common.Helpers;
using Users.Core.Repositories;
using Users.Common.Services.Interfaces;

namespace Users.Application.CQRS.Employees.Commands.Base
{
    public interface IDeleteEntityCommand
    {
        public Guid Id { get; set; }
    }

    public abstract class BaseDeleteEntityCommand : IRequest, IDeleteEntityCommand
    {
        public Guid Id { get; set; }
    }

    public abstract class BaseDeleteEntityCommandHandler<TCommand, TEntity> : IRequestHandler<TCommand>
        where TCommand : BaseDeleteEntityCommand
        where TEntity : class, IEntity<Guid>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserAccessor? _userAccessor;

        protected BaseDeleteEntityCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected BaseDeleteEntityCommandHandler(IDbContext dbContext, IUserAccessor userAccessor) : this(dbContext)
        {
            _userAccessor = userAccessor;
        }

        public virtual async Task Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entityToDelete = await _dbContext.Set<TEntity>()
                                                .FirstOrDefaultAsync(i => i.Id == request.Id);

            if (entityToDelete == null)
            {
                throw new Exception("Not found");
            }

            //CheckPermissions(request, entityToDelete);

            try
            {
                _dbContext.Set<TEntity>().Remove(entityToDelete);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Resource has related entities and therefore cannot be deleted.");
            }
        }

        //protected virtual void CheckPermissions(TCommand request, TEntity entity)
        //{
        //    if (_userAccessor != null && !AccessHelper.IsAuthorized(request, entity, _userAccessor))
        //        throw new AccessException("User not authrorized to perform delete.");
        //}
    }
}
