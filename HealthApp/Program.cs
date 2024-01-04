using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using HealthApp.Application;
using HealthApp.Infrastructure.Repostitories;
using HealthApp.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application).GetTypeInfo().Assembly));

//builder.Services.AddAutoMapper(typeof(Application));


var app = builder.Build();

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
