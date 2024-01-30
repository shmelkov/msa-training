using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/environment")]
        public EnvironmentConfig GetEnvironmentConfig()
        {
            return new EnvironmentConfig()
            {
                POSTGRES_HOST = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_HOST")) ? Environment.GetEnvironmentVariable("POSTGRES_HOST") : "undefined",
                POSTGRES_PORT = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_PORT")) ? Environment.GetEnvironmentVariable("POSTGRES_PORT") : "undefined",
                POSTGRES_USER = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_USER")) ? Environment.GetEnvironmentVariable("POSTGRES_USER") : "undefined",
                POSTGRES_PASSWORD = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")) ? Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") : "undefined",
            };
        }
    }
}