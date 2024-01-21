using AutoMapper;
using MassTransit;
using Portal.Common.Entities.Base;
using Portal.Common.Events;

namespace Portal.Common.MQ
{
    public abstract class BaseApplicationUserCreatedConsumer<T> : IConsumer<ApplicationUserCreatedEvent>
        where T : class, IApplicationUser
    {

        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        protected BaseApplicationUserCreatedConsumer(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task Consume(ConsumeContext<ApplicationUserCreatedEvent> context)
        {
            var @event = context.Message;

            var user = await _dbContext.Set<T>().FindAsync(@event.Id);

            if (user == null)
            {
                user = _mapper.Map<T>(@event);

                await _dbContext.Set<T>().AddAsync(user);
            }
            else
            {
                user = _mapper.Map(@event, user);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
