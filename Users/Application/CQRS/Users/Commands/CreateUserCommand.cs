using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
//using Portal.Common.CQRS.Commands;
using Users.Common.Events;
//using Portal.Users.Core;
using Users.Core.Entities;
using System.Transactions;
using static System.Net.Mime.MediaTypeNames;
using Users.Core.Repositories;

using Users.Application.CQRS.Employees.Commands.Base;

namespace Users.Application.CQRS.Users.Commands
{
    public class CreateUserCommand : BaseCreateEntityCommand
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public Guid? EmployeeId { get; set; }
    }

    public class CreateUserCommandHandler : BaseCreateEntityCommandHandler<CreateUserCommand, ApplicationUser>
    {
        private readonly IMapper _mapper;
        private readonly IUsersDbContext _dbContext;
        private readonly IPublisher _publisher;

        public CreateUserCommandHandler(IMapper mapper, IUsersDbContext dbContext, IPublisher publisher) : base(dbContext, mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async override Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Guid userId;

            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                userId = await base.Handle(request, cancellationToken);

                var user = await _dbContext.Users.FirstAsync(i => i.Id == userId);

                var @event = _mapper.Map<ApplicationUserCreatedEvent>(user);

                await _publisher.Publish(@event, source.Token);

                scope.Complete();
            }

            return userId;
        }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty();
            RuleFor(i => i.UserName).NotEmpty();
        }
    }
}
