using Portal.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Portal.Inftrastructure;
using static System.Net.Mime.MediaTypeNames;
using Portal.Core;
using Portal.Core.Repositories;
using Portal.Inftrastructure.Repositories;
using Portal.Extensions;
using MediatR;
using System.Reflection;
using FluentValidation;
using MassTransit;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
//using Portal.Common.Behaviors;
using Prometheus;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Add MrediatR
builder.Services.AddMediatR((c) =>
{
    c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    //c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    //c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PaginationBehavior<,>));
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
#endregion

builder.Services.AddControllers();

#region Add database context
var defaultConnection = string.Format("Host={0};Port={1};Database=News;Username={2};Password={3}",
    Environment.GetEnvironmentVariable("POSTGRES_HOST"),
    Environment.GetEnvironmentVariable("POSTGRES_PORT"),
    Environment.GetEnvironmentVariable("POSTGRES_USER"),
    Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));

builder.Services.AddDbContext<NewsContext>(options =>
               options.UseNpgsql(defaultConnection));
/*
builder.Services.AddDbContext<NewsContext>(options =>
               options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));
*/

builder.Services.AddScoped<INewsDbContext, NewsContext>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Adding Prometheus to collect mehtrics
app.UseHttpMetrics();

app.UseAuthorization();

//Adding for Prometheus
app.MapMetrics();

app.MapControllers();

try
{
    // Ensures DB is created against container
    app.EnsureDatabaseCreated<NewsContext>();
}
catch (Exception ex)
{ 
}

app.Run();
