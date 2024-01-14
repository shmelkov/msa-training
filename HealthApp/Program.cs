using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using HealthApp.Application;
using HealthApp.Infrastructure.Repostitories;
using HealthApp.Core;
using Microsoft.AspNetCore.Hosting;
using MediatR;
using HealthApp;
using HealthApp.Core.Repositories;
using HealthApp.Application.Commands;
using System.Net.NetworkInformation;


using System;
using System.IO;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using HealthApp.Application.Queries;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Configuration.Add(ConnectionInfo)
//builder.Services.AddScoped<IUserRepository, UserRepository>();

string connection = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4}",
    Environment.GetEnvironmentVariable("POSTGRES_HOST"),
    Environment.GetEnvironmentVariable("POSTGRES_PORT"),
    Environment.GetEnvironmentVariable("POSTGRES_DATABASE"),
    Environment.GetEnvironmentVariable("POSTGRES_USER"),
    Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));

//builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connection));
// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
               options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Configuration.AddEnvironmentVariables(connection);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application).GetTypeInfo().Assembly));
//builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies([typeof(Program).Assembly, typeof(xxxxxx).Assembly]); });

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}
*/

//builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(HealthApp.Controllers.UserController).Assembly));

/*
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
   
});
*/



//builder.Services.AddScoped(typeof(IGetEntityByIdQuery), typeof(GetUserByIdQuery));

//builder.Services.AddScoped(typeof(IGetEntityByIdQuery), typeof(BaseGetEntityByIdQuery<TDto>));




//builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

//builder.Services.AddSingleton<UserController>();

/*
builder.Services.AddMediatR((c) =>
{
    c.RegisterServicesFromAssembly(typeof(ApplicationStart).Assembly);
    c.RegisterServicesFromAssembly(typeof(HealthApp.Application.Queries.GetUserByIdQuery).Assembly);
    c.RegisterServicesFromAssembly(typeof(HealthApp.Application.DTOs.UserDto).Assembly);

});
*/

//Application Service
//builder.Services.AddSingleton<IUserRepository, UserRepository>();

/*
builder.Services.AddMediatR((c) =>
{
    c.RegisterServicesFromAssembly(typeof(Program).Assembly);

});
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseMicroservice();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
