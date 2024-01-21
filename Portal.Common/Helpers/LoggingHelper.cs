using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.OpenSearch;

namespace Portal.Common.Helpers
{
    public class LoggingHelper
    {
        public static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("PORTAL_ENVIRONMENT") ?? "dev";

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                             .Enrich.FromLogContext()
                             .Enrich.WithCorrelationIdHeader("portal-correlation-id")
                             .Enrich.WithExceptionDetails()
                             .WriteTo.Console()
                             //.WriteTo.OpenSearch(ConfigureOpenSearchSink(configuration, environment))
                             .Enrich.WithProperty("Environment", environment)
                             .ReadFrom.Configuration(configuration)
                             .CreateLogger();
        }

        private static OpenSearchSinkOptions ConfigureOpenSearchSink(IConfigurationRoot configuration, string environment)
        {
            return new OpenSearchSinkOptions(new Uri(configuration["OpenSearchConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"portal-{environment}-logs-{DateTime.UtcNow:yyyy-MM}"
            };
        } 
    }
}
