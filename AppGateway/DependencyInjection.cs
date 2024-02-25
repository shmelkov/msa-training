using AppGateway.Helpers;
using Refit;

namespace AppGateway
{
    public static class DependencyInhection
    {
        public static void InjectDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<TokenHelper>();
            builder.Services.AddTransient<AuthHelper>();

            builder.Services.AddRefitClient<IUserServiceApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://users"));
        }
    }
}
