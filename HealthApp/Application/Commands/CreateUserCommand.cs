using AutoMapper;
using MediatR;
using HealthApp.Core.Repositories;

using HealthApp.Core.Entities;
using HealthApp.Application.DTOs;

namespace HealthApp.Application.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IPublisher _publisher;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository repository, IPublisher publisher)
        {
            _mapper = mapper;
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<User>(request);

            newUser = await _repository.AddAsync(newUser);
            await _repository.DeleteAsync(newUser);

            return _mapper.Map<UserDto>(newUser);
        }
    }
}
