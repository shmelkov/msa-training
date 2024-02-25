using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Portal.Common.Middlewares;
//using Serilog;

namespace Users.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void EnsureDatabaseCreated<T>(this WebApplication app) where T : DbContext
        {
            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<T>();
            dbContext.Database.EnsureCreated();
        }
    }
}
