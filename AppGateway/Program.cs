using Ocelot.Middleware;
using Prometheus;
using AppGateway.Middlewares;
using AppGateway;
using Ocelot.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using AppGateway.Helpers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

var openIdOptions = builder.Configuration.GetSection(nameof(OpendIdOptions)).Get<OpendIdOptions>();
//var openIdOptions = new OpendIdOptions() { };

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, cfg =>
{
    cfg.SessionStore = new MemoryCacheTicketStore();
})
.AddOpenIdConnect(options =>
{
    options.Authority = openIdOptions.Authority;
    options.ClientId = openIdOptions.ClientId;
    options.ClientSecret = openIdOptions.ClientSecret;
    options.ResponseType = openIdOptions.ResponseType;
    options.SaveTokens = true;
    options.RequireHttpsMetadata = false;
    options.NonceCookie.SameSite = SameSiteMode.Unspecified;
    options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    options.Events = new OpenIdConnectEvents
    {
        OnTicketReceived = async context =>
        {
            var tokenGenerator = context.HttpContext.RequestServices.GetService<TokenHelper>();

            var token = await tokenGenerator.GetJwtToken(context.Principal);

            var authHelper = context.HttpContext.RequestServices.GetService<AuthHelper>();

            authHelper.AddTokenToCookies(context.Response, token);
        },
        //OnTokenValidated = async context =>
        //{
        //    var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
        //    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    claimsIdentity.AddClaim(new Claim("userId", userId));
        //},
        OnSignedOutCallbackRedirect = async context =>
        {
            context.HttpContext.Response.Cookies.Delete(AuthOptions.Cookie.Name);
        }
    };
});

builder.InjectDependencies();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerForOcelot(builder.Configuration, (o) =>
{
    o.GenerateDocsForGatewayItSelf = true;
});

//ocelot
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerForOcelotUI(opt =>
    {
        opt.PathToSwaggerGenerator = "/swagger/docs";
    });

    app.UseDeveloperExceptionPage();

    #region CORS

    app.UseCors(builder => builder.WithOrigins("http://localhost:3003")
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders("Content-Disposition"));

    app.UseCors(builder => builder.AllowCredentials());
    #endregion
}

//app.UseHttpsRedirection();

//Adding Prometheus to collect mehtrics
app.UseHttpMetrics();

app.UseMiddleware<JwtTokenFromCookieMiddleware>();

app.UseMiddleware<SetCorrelationIdMiddleware>();

app.UseRouting();

app.UseAuthorization();

//Adding for Prometheus
app.MapMetrics();


//app.MapControllers();
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#pragma warning restore ASP0014 // Suggest using top level route registrations


app.UseOcelot().Wait();

app.Run();
