//using MassTransit.Mediator;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Core.Repositories;
using Portal.Application.CQRS.News.Queries;
using System.Net;
using System.Web.Http.Results;
//using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/GenerateRandomErrors")]
        public async Task<IActionResult> GenerateRandomErrors()
        {
            Random rnd = new Random();
            int a = rnd.Next(1, 10);

            return (a < 5) ?
                StatusCode(500)
                //throw new System.Web.Http.HttpResponseException(HttpStatusCode.InternalServerError)
                :
                StatusCode(401);
                //HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                //throw new System.Web.Http.HttpResponseException(HttpStatusCode.Unauthorized);  

        }

        [HttpGet("/GenerateCustomErrors")]
        public async Task<IActionResult> GenerateCustomErrors()
        {
            Random rnd = new Random();
            int a = rnd.Next(1, 10);

            return (a < 5) ?
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.Forbidden)
                : throw new System.Web.Http.HttpResponseException(HttpStatusCode.GatewayTimeout);

        }

        [HttpGet("/GenerateLongResponse")]
        public async Task<IActionResult> GenerateLongResponse()
        {
            Random rnd = new Random();
            int a = rnd.Next(300, 10000);
            Thread.Sleep(a);
            return Ok(string.Format("sleep for {0} ms",a));
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
