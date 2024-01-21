using Microsoft.AspNetCore.Builder;
using Portal.Common.MQ;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Portal.Common.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureMQ(this WebApplicationBuilder builder, Assembly applicationAssembly)
        {
            var mqOptions = builder.Configuration.GetSection(nameof(MqOptions)).Get<MqOptions>();

            builder.Services.ConfigureMQ(applicationAssembly, mqOptions);
        }

        public static void AddLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
                logging.MediaTypeOptions.AddText("application/json");

                logging.ResponseBodyLogLimit = 1024;
                logging.RequestBodyLogLimit = 1024;
            });
        }
    }
}
