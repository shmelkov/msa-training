using AutoMapper;
using Users.Common.Events;
using Users.Core.Entities;
//using Portal.Users.Core;
using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Users.Core.Repositories;

using Users.Application.CQRS.Employees.Commands.Base;

namespace Users.Application.CQRS.Users.Commands
{
    public class UpdateUserCommand : BaseUpdateEntityCommand
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public Guid? EmployeeId { get; set; }
    }

    public class UpdateUserCommandHandler : BaseUpdateEntityCommandHandler<UpdateUserCommand, ApplicationUser>
    {
        private readonly IMapper _mapper;
        private readonly IUsersDbContext _dbContext;
        private readonly IPublisher _publisher;

        public UpdateUserCommandHandler(IMapper mapper, IUsersDbContext dbContext, IPublisher publisher) : base(dbContext, mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async override Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await base.Handle(request, cancellationToken);

                var user = await _dbContext.Users.FirstAsync(i => i.Id == request.Id);

                var @event = _mapper.Map<ApplicationUserUpdatedEvent>(user);

                await _publisher.Publish(@event, source.Token);

                scope.Complete();
            }
        }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty();
            RuleFor(i => i.FirstName).NotEmpty();
            RuleFor(i => i.LastName).NotEmpty();
            RuleFor(i => i.Email).NotEmpty();
            RuleFor(i => i.UserName).NotEmpty();
        }
    }
}
