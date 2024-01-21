using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Portal.Common.Behaviors;
using Portal.Common.Binders;
using Portal.Common.Filters;
using Portal.Common.MQ;
using Portal.Common.Services;
using Portal.Common.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Portal.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = false,
                        SignatureValidator = (token, parameters) => new JwtSecurityToken(token),
                        RequireExpirationTime = false,
                        ValidateLifetime = false,
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = false
                    };
                });
        }

        public static void ConfigureMQ(this IServiceCollection services, Assembly applicationAssembly, MqOptions options)
        {
            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumers(applicationAssembly);

                cfg.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter(options.QueuePrefix, false));

                cfg.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(options.Host, cfg =>
                    {
                        cfg.Username(options.Username);
                        cfg.Password(options.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<BaseRequestParametersFilter>();
            });
        }

        public static void ConfigureMicroservice(this IServiceCollection services, Assembly applicationAssembly)
        {
            services.ConfigureAuthentication();

            services.AddMediatR((c) =>
            {
                c.RegisterServicesFromAssembly(applicationAssembly);
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                c.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PaginationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(applicationAssembly);

            services.AddAutoMapper(applicationAssembly);

            services.ConfigureSwagger();

            services.AddHttpContextAccessor();

            services.AddScoped<IUserAccessor, UserAccessor>();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new SortingModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new PagingModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new FilteringModelBinderProvider());
            });
        }
    }
}
