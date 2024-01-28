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
builder.Services.AddDbContext<NewsContext>(options =>
               options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllers();

// Ensures DB is created against container
app.EnsureDatabaseCreated<NewsContext>();

app.Run();
