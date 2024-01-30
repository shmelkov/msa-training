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

        [HttpGet(Name = "GetEnvironments")]
        public string GetEnvironments()
        {
            return
                string.Format(
                    "POSTGRES_HOST={0}; " +
                    "POSTGRES_PORT={1}; " +
                    "POSTGRES_USER={2}; " +
                    "POSTGRES_PASSWORD={3};",
                Environment.GetEnvironmentVariable("POSTGRES_HOST"),
                Environment.GetEnvironmentVariable("POSTGRES_PORT"),
                Environment.GetEnvironmentVariable("POSTGRES_USER"),
                Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")
                );
        }
    }
}