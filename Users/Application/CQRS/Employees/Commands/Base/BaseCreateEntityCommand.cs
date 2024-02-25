using AutoMapper;
using MediatR;
using Users.Core.Repositories;
//using Portal.Common.Exceptions;
//using Portal.Common.Helpers;
//using Users.Core.Entities.Base;
using Users.Core.Entities;
using Users.Common.Services.Interfaces;

namespace Users.Application.CQRS.Employees.Commands.Base
{
    public interface ICreateEntityCommand
    {
        public Guid? Id { get; set; }
    }

    public abstract class BaseCreateEntityCommand : IRequest<Guid>, ICreateEntityCommand
    {
        public Guid? Id { get; set; }
    }

    public abstract class BaseCreateEntityCommandHandler<TCommand, TEntity> : IRequestHandler<TCommand, Guid>
        where TEntity : class, IEntity<Guid>
        where TCommand : BaseCreateEntityCommand
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private IUserAccessor? _userAccessor;

        protected BaseCreateEntityCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        protected BaseCreateEntityCommandHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor) : this(dbContext, mapper)
        {
            _userAccessor = userAccessor;
        }

        public virtual async Task<Guid> Handle(TCommand request, CancellationToken cancellationToken)
        {
            //CheckPermissions(request);

            var newEntity = _mapper.Map<TEntity>(request);

            await _dbContext.Set<TEntity>().AddAsync(newEntity);

            await _dbContext.SaveChangesAsync();

            return newEntity.Id;
        }

        //protected virtual void CheckPermissions(TCommand request)
        //{
        //    if (_userAccessor != null && !AccessHelper.IsAuthorized(request, _userAccessor))
        //        throw new AccessException("User not authrorized to perform create.");
        //}
    }
}
