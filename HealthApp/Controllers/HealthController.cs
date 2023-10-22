using Microsoft.AspNetCore.Mvc;
using HealthApp.Models;

namespace HealthApp.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{

    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "health")]
    public HealthResponse Get()
    {
        return new HealthResponse() {
            status = "OK"
        };
    }
}
