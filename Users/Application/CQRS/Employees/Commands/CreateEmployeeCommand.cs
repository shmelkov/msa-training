using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.CQRS.Employees.Commands.Base;
using Users.Common.Events;
//using Portal.Users.Core;
using Users.Core.Entities;
using System.Transactions;
using Users.Core.Repositories;

namespace Users.Application.CQRS.Employees.Commands
{
    public class CreateEmployeeCommand : BaseCreateEntityCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        //public string? MobilePhone { get; set; }
        //public DateTime? Birthdate { get; set; }
        //public string PersonnelNumber { get; set; }
        //public string? Location { get; set; }
        //public string? WorkPhone { get; set; }
        //public string? PhotoUrl { get; set; }
        //public Guid? CompanyId { get; set; }
        //public bool IsFired { get; set; }
        //public DateTime? EmploymentDate { get; set; }
    }

    public class CreateEmployeeCommandHandler : BaseCreateEntityCommandHandler<CreateEmployeeCommand, Employee>
    {
        private readonly IMapper _mapper;
        private readonly IUsersDbContext _dbContext;
        private readonly IPublisher _publisher;

        public CreateEmployeeCommandHandler(IMapper mapper, IUsersDbContext dbContext, IPublisher publisher) : base(dbContext, mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async override Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Guid employeeId;

            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                employeeId = await base.Handle(request, cancellationToken);

                var employee = await _dbContext.Employees
                    //.Include(i => i.Positions)
                    //.ThenInclude(p => p.Department)
                    .FirstAsync(i => i.Id == employeeId);

                var @event = _mapper.Map<EmployeeCreatedEvent>(employee);

                await _publisher.Publish(@event, source.Token);

                scope.Complete();
            }

            return employeeId;
        }
    }

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(i => i.FirstName).NotEmpty();
            RuleFor(i => i.LastName).NotEmpty();
            //RuleFor(i => i.PersonnelNumber).NotEmpty();
        }
    }
}
