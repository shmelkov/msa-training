using AutoMapper;
//using Portal.Common.CQRS.Commands;
//using Portal.Common.Events;
using Users.Core.Entities;
//using Portal.Users.Core;
using System.Transactions;
using MediatR;
using FluentValidation;
using Users.Core.Repositories;
using Users.Application.CQRS.Employees.Commands.Base;
using Users.Common.Events;

namespace Users.Application.CQRS.Users.Commands
{
    public class DeleteUserCommand : BaseDeleteEntityCommand
    {
    }

    public class DeleteUserCommandHandler : BaseDeleteEntityCommandHandler<DeleteUserCommand, ApplicationUser>
    {
        private readonly IMapper _mapper;
        private readonly IUsersDbContext _dbContext;
        private readonly IPublisher _publisher;

        public DeleteUserCommandHandler(IMapper mapper, IUsersDbContext dbContext, IPublisher publisher) : base(dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async override Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await base.Handle(request, cancellationToken);

                var @event = new ApplicationUserDeletedEvent() { Id = request.Id };

                await _publisher.Publish(@event, source.Token);

                scope.Complete();
            }
        }
    }

    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty();
        }
    }
}
