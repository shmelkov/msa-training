using MassTransit;
using Microsoft.EntityFrameworkCore;
using Portal.Common.Entities.Base;
using Portal.Common.Events;

namespace Portal.Common.MQ
{
    public abstract class BaseEmployeeDeletedConsumer<T> : IConsumer<EmployeeDeletedEvent>
        where T : class, IEmployee, IEntity<Guid>
    {
        private readonly IDbContext _dbContext;

        protected BaseEmployeeDeletedConsumer(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task Consume(ConsumeContext<EmployeeDeletedEvent> context)
        {
            var @event = context.Message;

            var employee = await _dbContext.Set<T>().FirstOrDefaultAsync(i => i.Id == @event.Id);

            if (employee != null)
            {
                try
                {
                    _dbContext.Set<T>().Remove(employee);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                }
            }
        }
    }

    public abstract class BaseEmployeeDeletedConsumer : BaseEmployeeDeletedConsumer<BaseEmployee>
    {
        protected BaseEmployeeDeletedConsumer(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
