//using MassTransit.Mediator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Core.Repositories;
using Portal.Application.CQRS.News.Queries;

namespace Portal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;
        private readonly IMediator _mediator;

        INewsRepository _newsRepository;

        public NewsController(INewsRepository repository, ILogger<NewsController> logger, IMediator mediator)
        {
            _newsRepository = repository;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetNewsByIdQuery() { Id = id });

            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetList([FromQuery] GetNewsQuery request)
        //{
        //    var result = await _mediator.Send(request);

        //    return Ok(result);
        //}

        //[HttpPost]
        //[Authorize(Policy = Policies.ElevatedRights)]
        //public async Task<IActionResult> Add(CreateNewsCommand request)
        //{
        //    var result = await _mediator.Send(request);

        //    return Ok(result);
        //}

        //[HttpDelete("{id}")]
        //[Authorize(Policy = Policies.ElevatedRights)]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _mediator.Send(new DeleteNewsCommand { Id = id });

        //    return Ok();
        //}

        //[Authorize(Policy = Policies.AdministratorRights)]
        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromQuery] DeleteNewsByIdsCommand request)
        //{
        //    await _mediator.Send(request);

        //    return Ok();
        //}

        //[HttpPut("{id}")]
        //[Authorize(Policy = Policies.ElevatedRights)]
        //public async Task<IActionResult> Update(Guid id, UpdateNewsCommand request)
        //{
        //    request.Id = id;
        //    await _mediator.Send(request);

        //    return Ok();
        //}

    }
}
