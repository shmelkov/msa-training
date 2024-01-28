//using AutoMapper;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Portal.Common.Entities.Base;
//using Portal.Common.Exceptions;
//using Portal.Common.Helpers;
//using Portal.Common.Services.Interfaces;
//using System.Text.Json.Serialization;

//namespace Portal.Common.CQRS.Commands
//{
//    public interface IUpdateEntityCommand
//    {   
//        public Guid Id { get; set; }
//    }

//    public abstract class BaseUpdateEntityCommand : IRequest, IUpdateEntityCommand
//    {
//        [JsonIgnore]
//        public Guid Id { get; set; }
//    }

//    public abstract class BaseUpdateEntityCommandHandler<TCommand, TEntity> : IRequestHandler<TCommand>
//        where TEntity : class, IEntity<Guid>
//        where TCommand : BaseUpdateEntityCommand
//    {
//        private readonly IDbContext _dbContext;
//        private readonly IMapper _mapper;
//        private readonly IUserAccessor? _userAccessor;

//        protected BaseUpdateEntityCommandHandler(IDbContext dbContext, IMapper mapper)
//        {
//            _dbContext = dbContext;
//            _mapper = mapper;
//        }

//        protected BaseUpdateEntityCommandHandler(IDbContext dbContext, IMapper mapper, IUserAccessor userAccessor) : this(dbContext, mapper)
//        {   
//            _userAccessor = userAccessor;
//        }

//        public virtual async Task Handle(TCommand request, CancellationToken cancellationToken)
//        {
//            var entityToUpdate = await _dbContext.Set<TEntity>()
//                                                 .FirstOrDefaultAsync(i => i.Id == request.Id)
//                                                 ?? throw new NotFoundException();

//            CheckPermissions(request, entityToUpdate);

//            entityToUpdate = _mapper.Map(request, entityToUpdate);

//            await _dbContext.SaveChangesAsync();
//        }

//        protected virtual void CheckPermissions(TCommand request, TEntity entity)
//        {
//            if (_userAccessor != null && !AccessHelper.IsAuthorized(request, entity, _userAccessor))
//                throw new AccessException("User not authrorized to perform update.");
//        }
//    }
//}
