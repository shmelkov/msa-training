using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portal.Common.Middlewares;
using Serilog;

namespace Portal.Common.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseMicroservice(this WebApplication app)
        {
            var isDevelopment = app.Environment.IsDevelopment();

            app.UseMiddleware<ExceptionHandlingMiddleware>(isDevelopment);

            if (isDevelopment)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static void EnsureDatabaseCreated<T>(this WebApplication app) where T: DbContext 
        {
            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<T>();
            dbContext.Database.EnsureCreated();
        }

        public static void UseLogging(this WebApplication app)
        {
            app.UseHttpLogging();

            app.UseSerilogRequestLogging();
        }
    }
}
