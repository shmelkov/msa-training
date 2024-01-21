using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Portal.Common.Entities.Base;
using Portal.Common.Events;

namespace Portal.Common.MQ
{
    public abstract class BaseEmployeeUpdatedConsumer<T> : IConsumer<EmployeeUpdatedEvent>
        where T : class, IEmployee, IEntity<Guid>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        protected BaseEmployeeUpdatedConsumer(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task Consume(ConsumeContext<EmployeeUpdatedEvent> context)
        {
            var @event = context.Message;

            var employee = await _dbContext.Set<T>().FirstOrDefaultAsync(i => i.Id == @event.Id);

            if (employee == null)
            {
                employee = _mapper.Map<T>(@event);

                await _dbContext.Set<T>().AddAsync(employee);
            }
            else
            {
                employee = _mapper.Map(@event, employee);
            }

            await _dbContext.SaveChangesAsync();
        }
    }

    public abstract class BaseEmployeeUpdatedConsumer : BaseEmployeeUpdatedConsumer<BaseEmployee>
    {
        private static IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmployeeUpdatedEvent, BaseEmployee>()
               .ForMember(i => i.FullName, src => src.MapFrom(i => $"{i.FirstName} {i.LastName}"));
        }));

        protected BaseEmployeeUpdatedConsumer(IDbContext dbContext) : base(dbContext, _mapper)
        {
        }
    }
}
