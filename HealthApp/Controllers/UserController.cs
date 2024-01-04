using Microsoft.AspNetCore.Mvc;
using HealthApp.Models;
using MediatR;
using HealthApp.Application.Commands;

namespace HealthApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;
    
    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    /*
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }
    */


    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = "user";// new GetUserByIdQuery { Id = id });

        return Ok(result);
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
    {
        await _mediator.Send(request);

        return Ok();
    }
    

}
