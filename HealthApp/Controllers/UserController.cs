using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HealthApp.Models;
using HealthApp.Application.Commands;
using HealthApp.Application.Queries;
using Infrastructure.EntityFramework;
using HealthApp.Core;

namespace HealthApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;
    private readonly DatabaseContext _context;

    /*

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    */


    public UserController(DatabaseContext dbContext)
    {
        _context = dbContext;
        //_logger = logger;
        //_mediator = mediator;
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        //var result = await _mediator.Send(new GetUserByIdQuery() { Id = id });
        var result = await new GetUserByIdQuery(_context).GetUserEntity(id);
        //var result = "test";

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
    {
        var result = "test";
        //await _mediator.Send(request);

        return Ok(result);
    }
    

}
