using Microsoft.AspNetCore.Mvc;
using HealthApp.Models;

namespace HealthApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PageVisitController : ControllerBase
{

    private readonly ILogger<HealthController> _logger;

    public PageVisitController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "add")]
    public HealthResponse AddVisit()
    {
        return new HealthResponse() {
            status = "OK"
        };
    }
}
