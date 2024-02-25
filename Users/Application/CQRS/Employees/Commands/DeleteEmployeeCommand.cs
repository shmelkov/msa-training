//using Portal.Common.CQRS.Commands;
//using Portal.Users.Core.Entities;
//using Portal.Users.Core;
//using MediatR;
//using Portal.Common.Events;
//using System.Transactions;
//using FluentValidation;
//using Users.Application.CQRS.Employees.Commands.Base;

//namespace Users.Application.CQRS.Employees.Commands
//{
//    public class DeleteEmployeeCommand : BaseDeleteEntityCommand
//    {
//    }
//    public class DeleteEmployeeCommandHandler : BaseDeleteEntityCommandHandler<DeleteEmployeeCommand, Employee>
//    {
//        private readonly IUsersDbContext _dbContext;
//        private readonly IPublisher _publisher;

//        public DeleteEmployeeCommandHandler(IUsersDbContext dbContext, IPublisher publisher) : base(dbContext)
//        {
//            _dbContext = dbContext;
//            _publisher = publisher;
//        }

//        public async override Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
//        {
//            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

//            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
//            {
//                await base.Handle(request, cancellationToken);

//                var @event = new EmployeeDeletedEvent() { Id = request.Id };

//                await _publisher.Publish(@event, source.Token);

//                scope.Complete();
//            }
//        }
//    }
//    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
//    {
//        public DeleteEmployeeCommandValidator()
//        {
//            RuleFor(i => i.Id).NotEmpty();
//        }
//    }
//}
