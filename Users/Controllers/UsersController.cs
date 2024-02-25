using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Users.Application.CQRS.Users.Commands;
using Users.Application.CQRS.Users.DTOs;
using Users.Application.CQRS.Users.Queries;

namespace Users.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        //private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator)
        {
            //_logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetUsersQuery request) => Ok(await _mediator.Send(request));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id) => Ok(await _mediator.Send(new GetUserByIdQuery { Id = id }));

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserDto>> GetByUserName(string userName) => Ok(await _mediator.Send(new GetUserByUserNameQuery { UserName = userName }));

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand request) => Ok(await _mediator.Send(request));

        ////[Authorize(Policy = Constants.PortalAdminRights)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand { Id = id });
            return Ok();
        }

        ////[Authorize(Policy = Constants.PortalAdminRights)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserCommand request)
        {
            request.Id = id;
            await _mediator.Send(request);
            return Ok();
        }

    }
}