using MassTransit;
using Microsoft.EntityFrameworkCore;
using Portal.Common.Entities.Base;
using Portal.Common.Events;

namespace Portal.Common.MQ
{
    public abstract class BaseApplicationUserDeletedConsumer<T> : IConsumer<ApplicationUserDeletedEvent>
        where T : class, IApplicationUser
    {

        private readonly IDbContext _dbContext;

        protected BaseApplicationUserDeletedConsumer(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Consume(ConsumeContext<ApplicationUserDeletedEvent> context)
        {
            var @event = context.Message;

            var user = await _dbContext.Set<T>().FindAsync(@event.Id);

            if (user != null)
            {
                try
                {
                    _dbContext.Set<T>().Remove(user);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                }
            }
        }
    }
}
